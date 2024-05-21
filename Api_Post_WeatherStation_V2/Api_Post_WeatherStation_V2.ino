#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include <DHT.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_BMP085_U.h>

#define DHTPIN D4
#define DHTTYPE DHT11 

#define ldrPin A0 
#define waterPin D5
#define speedSensorPin D6 // Hız sensörünün bağlı olduğu pin

float humidity, temperature, bmpTemperature, pressure;
int waterValue, sunValue, threshold = 50;
int randomNumber, randomNumber1;
double basinc;

DHT dht(DHTPIN, DHTTYPE);
Adafruit_BMP085_Unified bmp = Adafruit_BMP085_Unified(10085);

volatile unsigned int pulseCount = 0; // Pulse sayacı
int lastSensorState = LOW; // Son sensör durumu

unsigned long lastTime;
unsigned long interval = 500; // Ölçüm aralığı (milisaniye)
unsigned long postInterval = 1000; // POST isteği aralığı (milisaniye)
unsigned long lastPostTime = 0;

float wheelCircumference = 2 * 3.1416 * 0.025; // Tekerlek çevresi (0.3 metre yarıçap)
float speed = 0; // Hız değişkeni

const char* ssid = "Everest_8CBA08";
const char* password = "4542387513tdnd";
const char* apiUrl = "https://192.168.1.105:45455/api/WeatherStations/Update";

void setup() {
  Serial.begin(115200);
  dht.begin();
  WiFi.begin(ssid, password);
  
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }

  Serial.println("WiFi connected");

  if (!bmp.begin()) {
    Serial.print("BMP180 sensörü bulunamadı. Bağlantıları kontrol edin.");
    while (1);
  }

  pinMode(speedSensorPin, INPUT_PULLUP); // Hız sensör pinini ayarla
  lastTime = millis();
}

void loop() {
  // Hız ölçümünü sürekli olarak gerçekleştirme
  measureSpeed();

  // 5 saniyede bir veri gönderme
  if (millis() - lastPostTime >= postInterval) {
    press();
    dht11();
    sunLight();
    waterLevel();
    post();
    Message();
    lastPostTime = millis();
  }
}

void measureSpeed() {
  int currentSensorState = digitalRead(speedSensorPin); // Mevcut sensör durumunu oku

  // Sensör durumu LOW'dan HIGH'a değiştiğinde pulse sayısını artır
  if (lastSensorState == LOW && currentSensorState == HIGH) {
    pulseCount++;
  }

  lastSensorState = currentSensorState; // Son sensör durumunu güncelle

  unsigned long currentTime = millis();
  if (currentTime - lastTime >= interval) {
    float timeInSeconds = interval / 1000.0; // Zamanı saniyeye çevir
    float frequency = (pulseCount / timeInSeconds); // Frekansı hesapla (pulse per second)
    speed = calculateSpeed(frequency); // Hızı hesapla
    pulseCount = 0; // Sayaç sıfırla
    lastTime = currentTime;
  }
}

float calculateSpeed(float frequency) {
  float speed = frequency * wheelCircumference * 3.6; // Hızı km/saat cinsine dönüştür
  return speed;
}

void post() {
  randomNumber = random(0, 100);
  randomNumber1 = random(0, 24);
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    WiFiClient client;
    http.begin(client, apiUrl);
    http.addHeader("Content-Type", "application/json");

    StaticJsonDocument<200> doc;
    doc["id"] = "839d0a97-5fff-465a-bf29-979890386f05";
    doc["weatherStationName"] = "İstasyon-No1";
    doc["windSpeed"] = speed; // Hız verisini ekle
    doc["windDirection"] = 2;
    doc["waterLevel"] = waterValue;
    doc["temperature"] = temperature;
    doc["humidity"] = humidity;
    doc["pressure"] = pressure;
    doc["sunLight"] = sunValue;
    doc["voltage"] = randomNumber1;
    doc["productCode"] = "SN1-WS1";
    doc["productTypeId"] = "2a05bb83-01f0-451d-9feb-b8f98e06b69b";

    String jsonString;
    serializeJson(doc, jsonString);

    int httpCode = http.POST(jsonString);

    if (httpCode > 0) {
      String payload = http.getString();
      Serial.println(payload);
    } else {
      Serial.print("HTTP POST request failed, error code: ");
      Serial.println(httpCode);
    }
    http.end();
  }
}

void dht11(){
  humidity = dht.readHumidity();    // Nem değerini oku
  temperature = dht.readTemperature(); // Sıcaklık değerini oku
}

void press(){
  // bmpTemperature = bmp.readTemperature(); // Sıcaklık değerini oku (°C)
  // pressure = bmp.readPressure() / 100.0F; // Basınç değerini oku (hektopaskal olarak)
  sensors_event_t event;
  bmp.getEvent(&event);

  if (event.pressure) {
    // Basınç ve sıcaklık verilerini al
    pressure = event.pressure;
    bmpTemperature;
    bmp.getTemperature(&bmpTemperature);

    // Rakımı hesapla
    float seaLevelPressure = 1013.25; // Standart deniz seviyesi basıncı (hPa)
    float altitude = 44330 * (1.0 - pow(pressure / seaLevelPressure, 1 / 5.255));

    // Verileri seri monitöre yazdır
    Serial.print("Basınç: ");
    Serial.print(pressure);
    Serial.print(" hPa, Sıcaklık: ");
    Serial.print(bmpTemperature);
    Serial.print(" C, Rakım: ");
    Serial.print(altitude);
    Serial.println(" m");
  }
}

void waterLevel(){
  waterValue = analogRead(waterPin); // Su seviye sensöründen okunan değeri al

  // Su seviyesi eşik değerinin altında ise su var, yoksa yok.
  if (waterValue > threshold) {
    waterValue = 1;
    Serial.println("Su var");
  } 
  else {
    waterValue = 0;
    Serial.println("Su yok");
  }
}

void sunLight(){
  sunValue = analogRead(ldrPin);
}

void Message(){
  Serial.print("Rüzgar Hızı: ");
  Serial.print(speed);
  Serial.println(" km/H");

  Serial.print("Sıcaklık(DHT11): ");
  Serial.print(temperature);
  Serial.println(" °C");

  Serial.print("Nem: ");
  Serial.print(humidity);
  Serial.println(" %");

  Serial.print("Sıcaklık(BMP): ");
  Serial.print(bmpTemperature);
  Serial.println(" °C");

  Serial.print("Basınç: ");
  Serial.print(pressure);
  Serial.println(" metre");

  Serial.print("Güneş Işığı: ");
  Serial.print(sunValue);
  Serial.println(" °X");

  Serial.print("Su Durumu: ");
  Serial.print(waterValue);
  Serial.println(" °X");

  Serial.print("Voltaj: ");
  Serial.print(randomNumber1);
  Serial.println(" V");

}
