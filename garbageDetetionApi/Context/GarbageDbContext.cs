using garbageDetetionApi.Models;
using LitterLinq.Configurations;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Context;

public class GarbageDbContext(DbContextOptions<GarbageDbContext> options) : DbContext(options)
{

    public DbSet<Garbage> Garbages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new LitterConfiguration());
    }
}