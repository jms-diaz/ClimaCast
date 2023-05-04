namespace ClimaCast.Data
{
    public class WeatherData
    {
        public string? Weather{ get; set; }
        public string? WeatherDescription { get; set; }
        public decimal Temperature { get; set; }
        public decimal FeelsLike { get; set; }
        public decimal Humidity { get; set; }
        public decimal MinTemperature { get; set; }
        public decimal MaxTemperature { get; set; }
    }
}
