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
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Detected = "Plastic Bottle",
                    Confidence_score = 0.95m,
                    Weather = "Sunny",
                    Temp = 22.5m,
                    Humidity = 45.0m,
                    Windspeed = 5.2m,
                    Timestamp = new DateTime(2025, 5, 15, 10, 0, 0)
                },
                new Garbage
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Detected = "Can",
                    Confidence_score = 0.89m,
                    Weather = "Cloudy",
                    Temp = 18.3m,
                    Humidity = 55.0m,
                    Windspeed = 3.8m,
                    Timestamp = new DateTime(2025, 5, 15, 11, 0, 0)
                },
                new Garbage
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Detected = "Paper",
                    Confidence_score = 0.92m,
                    Weather = "Rainy",
                    Temp = 16.0m,
                    Humidity = 80.0m,
                    Windspeed = 7.1m,
                    Timestamp = new DateTime(2025, 5, 15, 12, 0, 0)
                },
                new Garbage
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Detected = "Glass",
                    Confidence_score = 0.88m,
                    Weather = "Windy",
                    Temp = 20.0m,
                    Humidity = 50.0m,
                    Windspeed = 12.0m,
                    Timestamp = new DateTime(2025, 5, 15, 13, 0, 0)
                },
                new Garbage
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Detected = "Food Wrapper",
                    Confidence_score = 0.90m,
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
