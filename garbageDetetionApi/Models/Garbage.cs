using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace garbageDetetionApi.Models
{
    public class Garbage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Detected { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public decimal Confidence_score { get; set; }

        [System.ComponentModel.DataAnnotations.Required]

        public Guid CameraId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Longitude { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Column(TypeName = "decimal(9,6)")]
        public decimal Latitude { get; set; }

        public string? Weather { get; set; }
        public decimal? Temp { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? Windspeed { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
