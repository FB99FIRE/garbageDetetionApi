using garbageDetetionApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LitterLinq.Configurations
{
    public class LitterConfiguration : IEntityTypeConfiguration<Garbage>
    {
        public void Configure(EntityTypeBuilder<Garbage> builder)
        {
            builder.ToTable("Garbages");
            builder.HasData(
                new Garbage
                {
                    Id = 1,
                    Detected = "Plastic Bottle",
                    Confidence_score = 0.95m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Sunny",
                    Temp = 22.5m,
                    Humidity = 45.0m,
                    Windspeed = 5.2m,
                    Timestamp = new DateTime(2025, 5, 15, 10, 0, 0)
                },
                new Garbage
                {
                    Id = 2,
                    Detected = "Can",
                    Confidence_score = 0.89m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Cloudy",
                    Temp = 18.3m,
                    Humidity = 55.0m,
                    Windspeed = 3.8m,
                    Timestamp = new DateTime(2025, 5, 15, 11, 0, 0)
                },
                new Garbage
                {
                    Id = 3,
                    Detected = "Paper",
                    Confidence_score = 0.92m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Rainy",
                    Temp = 16.0m,
                    Humidity = 80.0m,
                    Windspeed = 7.1m,
                    Timestamp = new DateTime(2025, 5, 15, 12, 0, 0)
                },
                new Garbage
                {
                    Id = 4,
                    Detected = "Glass",
                    Confidence_score = 0.88m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Windy",
                    Temp = 20.0m,
                    Humidity = 50.0m,
                    Windspeed = 12.0m,
                    Timestamp = new DateTime(2025, 5, 15, 13, 0, 0)
                },
                new Garbage
                {
                    Id = 5,
                    Detected = "Food Wrapper",
                    Confidence_score = 0.90m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Sunny",
                    Temp = 24.0m,
                    Humidity = 40.0m,
                    Windspeed = 4.5m,
                    Timestamp = new DateTime(2025, 5, 15, 14, 0, 0)
                }
            );
        }
    }
}
