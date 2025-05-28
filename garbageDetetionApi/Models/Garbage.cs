namespace garbageDetetionApi.Models
{
    public class Garbage
    {
        public int Id { get; set; }
        public string Detected { get; set; }
        public decimal Confidence_score { get; set; }

        public string Weather {  get; set; }

        public decimal Temp { get; set; }

        public decimal Humidity { get; set; }
        public decimal Windspeed {  get; set; }

        public DateTime Timestamp { get; set; }
    }
}
