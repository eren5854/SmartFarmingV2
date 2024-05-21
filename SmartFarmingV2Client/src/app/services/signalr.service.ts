import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  sensorHub: signalR.HubConnection | undefined;
  weatherStationHub : signalR.HubConnection | undefined;
  constructor() { }

  startSensorHubConnection(callBack: () => void) {
    this.sensorHub = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44392/sensor-hub")
      .build();

    this.sensorHub
      .start()
      .then(() => {
        console.log("Connection started");
        callBack();
      })
      .catch((err: any) => console.log(err));
  }

  startWeatherStationHubConnection(callBack: () => void) {
    this.weatherStationHub = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:44392/weatherStation-hub")
      .build();

    this.weatherStationHub
      .start()
      .then(() => {
        console.log("Connection started");
        callBack();
      })
      .catch((err: any) => console.log(err));
  }
}
