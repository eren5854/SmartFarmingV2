export class WeatherStationModel{
    id: string ="";
    weatherStationName: string = "";
    productCode: string = "";
    productTypeId: string = "";
    updatedDate: Date = new Date();
    updatedBy: string = "";
    windSpeed: number = 0;
    windDirection: number = 0;
    waterLevel: number = 0;
    pressure: number = 0;
    temperature: number = 0;
    humidity: number = 0;
    sunLight: number = 0;
    voltage: number = 0;
}