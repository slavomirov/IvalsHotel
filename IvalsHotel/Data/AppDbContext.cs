using IvalsHotel.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IvalsHotel.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    {
    }

    public DbSet<Tire> Tires { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Tire>()
            .HasKey(x => x.Id);

        builder.Entity<Log>()
            .HasKey(x => x.Id);

        builder.Entity<Log>()
            .Property(x => x.Type)
            .HasConversion<string>()
            .HasMaxLength(15);
    }
}
