using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace DbContextService; 

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PackageInformation>()
            .HasOne(p => p.Sender)
            .WithOne(s => s.Package)
                .HasForeignKey<SenderInformation>(s => s.PackageRef);

        modelBuilder.Entity<PackageInformation>()
            .HasOne(p => p.Recipient)
            .WithOne(s => s.Package)
                .HasForeignKey<RecipientInformation>(s => s.PackageRef);

        modelBuilder.Entity<PackageInformation>()
            .HasMany(p => p.TimeStampHistories)
            .WithOne(s => s.Package)
            .HasForeignKey(s => s.PackageRef);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<PackageInformation> PackageInformations { get; set; } = null!;
    public DbSet<StatusHistory> StatusHistories { get; set; } = null!;


}
