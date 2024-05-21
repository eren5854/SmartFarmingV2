import { AfterViewInit, Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { SignalrService } from '../../services/signalr.service';
import { SensorModel } from '../../models/sensor.model';
import { DatePipe } from '@angular/common';
import { WeatherStationModel } from '../../models/weatherStation.model';
declare const Chart: any;
declare const echarts: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SharedModule],
  providers: [DatePipe],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements AfterViewInit {
  sensors: SensorModel[] = [];
  sensors1: SensorModel[] = [];
  weatherStation: WeatherStationModel[] = [];
  chart: any;
  speedometerChart: any;
  isWaterLevelHigh: number = 0;
  isRelayHigh: number = 0;

  constructor(
    private signalR: SignalrService,
    private date: DatePipe
  ) {
    this.signalR.startSensorHubConnection(() => {
      this.signalR.sensorHub?.on("Sensor", (res: SensorModel) => {
        if (res.productCode === "SN1-GHS") {
          this.sensors.push(res);
          console.log(res.productCode);
          this.showGroundHumidityChart(res.sensorData);
        }
        else if (res.productCode === "SN1-VAL") {
          this.sensors1.push(res);
          console.log(res.productCode);
          this.showRelayStatusChart(res.sensorData);
        }

        // const labels = this.sensors.map((val) => this.date.transform(val.updatedDate, "HH:mm:ss") ?? "00:00:00");
        // this.chart.data.labels = labels;
        // const sensorData = this.sensors.map((val) => val.sensorData);
        // const sensorData1 = this.sensors1.map((val) => val.sensorData);
        // this.chart.data.datasets[0].data = sensorData;
        // this.chart.data.datasets[1].data = sensorData1;
        // this.chart.update();
        console.log(res);
      })
    });

    this.signalR.startWeatherStationHubConnection(() => {
      this.signalR.weatherStationHub?.on("WeatherStation", (res: WeatherStationModel) => {
        if (res.productCode === "SN1-WS1") {
          this.weatherStation.push(res);
          console.log(res.productCode);

          const labels = this.weatherStation.map((val) => this.date.transform(val.updatedDate, "HH:mm:ss") ?? "00:00:00");
          this.chart.data.labels = labels;
          const voltage = this.weatherStation.map((val) => val.voltage);
          this.chart.data.datasets[0].data = voltage;
          
          
          this.showTemperatureChart(res.temperature);
          this.showHumidityChart(res.humidity);
          this.showSunLightChart(res.sunLight);
          this.showPressureChart(res.pressure);
          this.showWindSpeedChart(res.windSpeed);
          this.showWaterLevelChart(res.waterLevel);

          this.chart.update();
        }
        console.log(res);
      })
    });
  }

  ngAfterViewInit(): void {
    this.showChart();
  }

  showChart() {
    const ctx = document.getElementById('myChart');

    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: [],
        datasets: [
          {
            label: '# Voltaj (V)',
            data: [],
            borderWidth: 1
          }
        ]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }

  setGaugeValue(gauge: Element | null, value: number) {
    if (!gauge) return;
    // if (value < 0 || value > 1) {
    //   return;
    // }

    const gaugeFill = gauge.querySelector(".gauge__fill") as HTMLElement;
    const gaugeCover = gauge.querySelector(".gauge__cover") as HTMLElement;

    if (gaugeFill && gaugeCover) {
      gaugeFill.style.transform = `rotate(${value / 2}turn)`;
      gaugeCover.textContent = `${Math.round(value * 100)}°C`;
    }
  }

  showTemperatureChart(value: number) {
    const speedometer = document.getElementById("speedometer");
    this.speedometerChart = echarts.init(speedometer);
    const option = {
      series: [
        {
          type: 'gauge',
          center: ['50%', '70%'],
          startAngle: 200,
          endAngle: -20,
          min: -20,
          max: 50,
          splitNumber: 14,
          itemStyle: {
            color: '#FFAB91'
          },
          progress: {
            show: true,
            width: 25
          },
          pointer: {
            show: false
          },
          axisLine: {
            lineStyle: {
              width: 25
            }
          },
          axisTick: {
            distance: -45,
            splitNumber: 5,
            lineStyle: {
              width: 2,
              color: '#999'
            }
          },
          splitLine: {
            distance: -52,
            length: 14,
            lineStyle: {
              width: 3,
              color: '#999'
            }
          },
          axisLabel: {
            distance: -20,
            color: '#999',
            fontSize: 18
          },
          anchor: {
            show: false
          },
          title: {
            show: true
          },
          detail: {
            valueAnimation: true,
            width: '50%',
            lineHeight: 40,
            borderRadius: 8,
            offsetCenter: [0, '-10%'],
            fontSize: 30,
            fontWeight: '',
            formatter: '{value} °C',
            color: 'auto'
          },
          data: [
            {
              value: value
            }
          ]
        },
        
      ]
    };
    this.speedometerChart.setOption(option);
  }

  showHumidityChart(value: number) {
    const humidity = document.getElementById("humidity");
    this.speedometerChart = echarts.init(humidity);

    const option = {
      series: [
        {
          type: 'gauge',
          center: ['50%', '70%'],
          startAngle: 200,
          endAngle: -20,
          min: 0,
          max: 100,
          splitNumber: 10,
          itemStyle: {
            color: '#FFAB91'
          },
          progress: {
            show: true,
            width: 25
          },
          pointer: {
            show: false
          },
          axisLine: {
            lineStyle: {
              width: 25
            }
          },
          axisTick: {
            distance: -45,
            splitNumber: 5,
            lineStyle: {
              width: 2,
              color: '#999'
            }
          },
          splitLine: {
            distance: -52,
            length: 14,
            lineStyle: {
              width: 3,
              color: '#999'
            }
          },
          axisLabel: {
            distance: -20,
            color: '#999',
            fontSize: 18
          },
          anchor: {
            show: false
          },
          title: {
            show: false
          },
          detail: {
            valueAnimation: true,
            width: '50%',
            lineHeight: 40,
            borderRadius: 8,
            offsetCenter: [0, '-10%'],
            fontSize: 30,
            fontWeight: '',
            formatter: '{value} %',
            color: 'auto'
          },
          data: [
            {
              value: value
            }
          ]
        }
      ]
    };
    this.speedometerChart.setOption(option);   
  }

  showSunLightChart(value: number) {
    const sunLight = document.getElementById("sunLight");
    this.speedometerChart = echarts.init(sunLight);

    const option = {
      series: [
        {
          type: 'gauge',
          startAngle: 180,
          endAngle: 0,
          center: ['50%', '75%'],
          radius: '90%',
          min: 0,
          max: 1023,
          splitNumber: 8,
          axisLine: {
            lineStyle: {
              width: 6,
              color: [
                [0.25, '#7CFFB2'],
                [0.5, '#58D9F9'],
                [0.75, '#FDDD60'],
                [1, '#FF6E76']
              ]
            }
          },
          pointer: {
            icon: 'path://M12.8,0.7l12,40.1H0.7L12.8,0.7z',
            length: '12%',
            width: 20,
            offsetCenter: [0, '-60%'],
            itemStyle: {
              color: 'auto'
            }
          },
          axisTick: {
            length: 12,
            lineStyle: {
              color: 'auto',
              width: 2
            }
          },
          splitLine: {
            length: 20,
            lineStyle: {
              color: 'auto',
              width: 5
            }
          },
          axisLabel: {
            color: '#464646',
            fontSize: 20,
            distance: -60,
            rotate: 'tangential',
            formatter: function (value: number) {
              if (value === 0.875) {
                return 'Grade A';
              } else if (value === 0.625) {
                return 'Grade B';
              } else if (value === 0.375) {
                return 'Grade C';
              } else if (value === 0.125) {
                return 'Grade D';
              }
              return '';
            }
          },
          title: {
            offsetCenter: [0, '-10%'],
            fontSize: 20
          },
          detail: {
            fontSize: 20,
            offsetCenter: [0, '-35%'],
            valueAnimation: true,
            formatter: function (value: number) {
              return Math.round(value * 1) + '';
            },
            color: 'inherit'
          },
          data: [
            {
              value: value,
              name: 'Güneş Işığı'
            }
          ]
        }
      ]
    };    
    this.speedometerChart.setOption(option);   
  }

  showPressureChart(value: number) {
    const pressure = document.getElementById("pressure");
    this.speedometerChart = echarts.init(pressure);

    const option = {
      tooltip: {
        formatter: '{a} <br/>{b} : {c}%'
      },
      series: [
        {
          name: 'Pressure',
          type: 'gauge',
          min: 0,
          max: 1500,
          splitNumber: 5,
          detail: {
            formatter: '{value}',
            fontSize: 14,
            offsetCenter: [0, '65%'],
          },
          title: {
            // offsetCenter: [0, '-10%'],
            fontSize: 14
          },
          data: [
            {
              value: value,
              name: 'hPa'
            }
          ]
        }
      ]
    };
    this.speedometerChart.setOption(option);   
  }

  showWindSpeedChart(value: number) {
    const windSpeed = document.getElementById("windSpeed");
    this.speedometerChart = echarts.init(windSpeed);

    const option = {
      series: [
        {
          type: 'gauge',
          axisLine: {
            lineStyle: {
              width: 15,
              color: [
                [0.3, '#67e0e3'],
                [0.7, '#37a2da'],
                [1, '#fd666d']
              ]
            }
          },
          pointer: {
            itemStyle: {
              color: 'auto'
            }
          },
          axisTick: {
            distance: -30,
            length: 8,
            lineStyle: {
              color: '#fff',
              width: 2
            }
          },
          splitLine: {
            distance: -30,
            length: 30,
            lineStyle: {
              color: '#fff',
              width: 4
            }
          },
          axisLabel: {
            color: 'inherit',
            distance: 20,
            fontSize: 14
          },
          detail: {
            fontSize: 18,
            valueAnimation: true,
            formatter: '{value} km/h',
            color: 'inherit'
          },
          data: [
            {
              value: Math.round(value)
            }
          ]
        }
      ]
    };
    // setInterval(function () {
    //   myChart.setOption({
    //     series: [
    //       {
    //         data: [
    //           {
    //             value: +(Math.random() * 100).toFixed(2)
    //           }
    //         ]
    //       }
    //     ]
    //   });
    // }, 2000);
    this.speedometerChart.setOption(option);   
  }

  showGroundHumidityChart(value: number) {
    const groundHumidity = document.getElementById("groundHumidity");
    this.speedometerChart = echarts.init(groundHumidity);

    const option = {
      series: [
        {
          type: 'gauge',
          axisLine: {
            lineStyle: {
              width: 15,
              color: [
                [0.3, '#67e0e3'],
                [0.7, '#37a2da'],
                [1, '#fd666d']
              ]
            }
          },
          pointer: {
            itemStyle: {
              color: 'auto'
            }
          },
          axisTick: {
            distance: -30,
            length: 8,
            lineStyle: {
              color: '#fff',
              width: 2
            }
          },
          splitLine: {
            distance: -30,
            length: 30,
            lineStyle: {
              color: '#fff',
              width: 4
            }
          },
          axisLabel: {
            color: 'inherit',
            distance: 20,
            fontSize: 14
          },
          detail: {
            fontSize: 18,
            valueAnimation: true,
            formatter: '{value} %',
            color: 'inherit'
          },
          data: [
            {
              value: value
            }
          ]
        }
      ]
    };
    // setInterval(function () {
    //   myChart.setOption({
    //     series: [
    //       {
    //         data: [
    //           {
    //             value: +(Math.random() * 100).toFixed(2)
    //           }
    //         ]
    //       }
    //     ]
    //   });
    // }, 2000);
    this.speedometerChart.setOption(option);   
  }

  showWaterLevelChart(value: number){
    this.isWaterLevelHigh = value;
  }

  showRelayStatusChart(value: number){
    this.isRelayHigh = value;
  }
}
