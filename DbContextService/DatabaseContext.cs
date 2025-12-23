using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
       
        base.OnModelCreating(modelBuilder);

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

        //Seeding database with dummy content.
        void AddSingleDummyEntity(int id)
        {
            PackageInformation package1 = new PackageInformation()
            {
                Id = id,
                //CurrentStatus = 0,

            };

            modelBuilder.Entity<PackageInformation>();

            modelBuilder.Entity<PackageInformation>().HasData(package1);

            SenderInformation sender1 = new SenderInformation()
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                Address = "Somest101",
                Phone = "888888888",
                PackageRef = id,
            };

            modelBuilder.Entity<SenderInformation>().HasData(sender1);

            RecipientInformation recipient1 = new RecipientInformation()
            {
                Id = id,
                FirstName = "Some",
                LastName = "Guy",
                Address = "Otherst010",
                Phone = "123456789",
                PackageRef = id

            };

            modelBuilder.Entity<RecipientInformation>().HasData(recipient1);

            StatusHistory history1 = new StatusHistory()
            {
                Id = id,
                PackageRef = id,
                Status = 0,
            };

            modelBuilder.Entity<StatusHistory>().HasData(history1);
        }

        for (int i = 1; i <= 10; i++) {
            AddSingleDummyEntity(i);
        }

    }

    public DbSet<PackageInformation> PackageInformations { get; set; }
    public DbSet<StatusHistory> StatusHistories { get; set; }


}
