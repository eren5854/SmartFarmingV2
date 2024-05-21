#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

const char* ssid = "Everest_8CBA08";
const char* password = "4542387513tdnd";
const char* apiUrl = "https://192.168.1.105:45455/api/Sensors/GetAll";
const char* targetId = "8c5a1a14-a0fb-428b-9f36-478e6e15933d";

#define relayPin 0

void setup() {
  Serial.begin(115200);
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }

  Serial.println("WiFi connected");
  pinMode(relayPin, OUTPUT); // Röle pini çıkış olarak ayarlandı
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    WiFiClient client;
    http.begin(client, apiUrl);
    int httpCode = http.GET();

    if (httpCode > 0) {
      String payload = http.getString();
      // Serial.println(payload);
      DynamicJsonDocument doc(2048);
      DeserializationError error = deserializeJson(doc, payload);

      if (!error) {
        JsonArray data = doc.as<JsonArray>();

        for (JsonObject obj : data) {
          const char* id = obj["id"];
          if (strcmp(id, targetId) == 0) {
            float sensorData = obj["sensorData"];
            
            Serial.print("Sensor Data for ID ");
            Serial.print(targetId);
            Serial.print(": ");
            Serial.println(sensorData);

            if(sensorData == 1){
              Serial.println("Valf Açık");
              digitalWrite(relayPin, HIGH);
            }
            else if(sensorData == 0){
              Serial.println("Valf Kapalı");
              digitalWrite(relayPin, LOW);
            }
            break; // Belirtilen ID bulundu, döngüden çık
          }
        }
      } else {
        Serial.print("deserializeJson() failed: ");
        Serial.println(error.c_str());
      }
    } else {
      Serial.print("HTTP GET request failed, error code: ");
      Serial.println(httpCode);
    }

    http.end();
  }

  delay(1000);
}
