using garbageDetetionApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LitterLinq.Configurations
{
    public class LitterConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.ToTable("ApiKeys");
            builder.HasData(
                new ApiKey
                { 
                    Id = Guid.Parse("12a12a12-a12a-12aa-12aa-12a12a12a12a"),
                    Type = "write"
                },
                new ApiKey
                { 
                    Id = Guid.Parse("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"),
                    Type = "read"
                }
            );
        }
    }
}
