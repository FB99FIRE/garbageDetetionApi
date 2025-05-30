namespace garbageDetetionApi.Models
{
    public class Garbage
    {
        public Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Detected { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal Confidence_score { get; set; }
         
        public string? Weather { get; set; }
        public decimal? Temp { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? Windspeed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
