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
                    DetectedObject = "Plastic Bottle",
                    ImageName = "plastic_bottle",
                    ConfidenceScore = 0.95m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Sunny",
                    Temp = 22.5m,
                    Humidity = 45.0m,
                    WindSpeed = 5.2m,
                    TimeStamp = new DateTime(2025, 5, 15, 10, 0, 0)
                },
                new Garbage
                {
                    Id = 2,
                    DetectedObject = "Can",
                    ImageName = "can",
                    ConfidenceScore = 0.89m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Cloudy",
                    Temp = 18.3m,
                    Humidity = 55.0m,
                    WindSpeed = 3.8m,
                    TimeStamp = new DateTime(2025, 5, 15, 11, 0, 0)
                },
                new Garbage
                {
                    Id = 3,
                    DetectedObject = "Paper",
                    ImageName = "paper",
                    ConfidenceScore = 0.92m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Rainy",
                    Temp = 16.0m,
                    Humidity = 80.0m,
                    WindSpeed = 7.1m,
                    TimeStamp = new DateTime(2025, 5, 15, 12, 0, 0)
                },
                new Garbage
                {
                    Id = 4,
                    DetectedObject = "Glass",
                    ImageName = "glass",
                    ConfidenceScore = 0.88m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Windy",
                    Temp = 20.0m,
                    Humidity = 50.0m,
                    WindSpeed = 12.0m,
                    TimeStamp = new DateTime(2025, 5, 15, 13, 0, 0)
                },
                new Garbage
                {
                    Id = 5,
                    DetectedObject = "Food Wrapper",
                    ImageName = "food_wrapper",
                    ConfidenceScore = 0.90m,
                    CameraId = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Longitude = 4.778720m,
                    Latitude = 51.591415m,
                    Weather = "Sunny",
                    Temp = 24.0m,
                    Humidity = 40.0m,
                    WindSpeed = 4.5m,
                    TimeStamp = new DateTime(2025, 5, 15, 14, 0, 0)
                }
            );
        }
    }
}
