using garbageDetetionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace garbageDetetionApi.Context;

public class GarbageDbContext(DbContextOptions<GarbageDbContext> options) : DbContext(options)
{
    public DbSet<Garbage> Garbages { get; set; }
}