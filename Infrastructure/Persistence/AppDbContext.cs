using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Spaceship.Infrastructure.Persistence.Entities;

namespace Spaceship.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherEntity> Weathers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherEntity>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<WeatherEntity>().Property(x => x.Unix).HasDefaultValueSql("DATEDIFF(s, '1970-01-01', GETUTCDATE())");
    }
}