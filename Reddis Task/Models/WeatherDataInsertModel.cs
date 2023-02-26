namespace Reddis_Task.Models
{
    public class WeatherDataInsertModel
    {
        public string City { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Pressure { get; set; }
    }
}
