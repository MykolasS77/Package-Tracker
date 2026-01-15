using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace PackageTracker.Server.Database; 


public class DatabaseContext : DbContext
{

    private readonly IConfiguration? _configuration;
    
    //Constructor without IConfiguration type argument used for database mocking in unit tests.
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        
    }
    public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PackageInformation>()
            .HasOne(p => p.SenderAndRecipientDetails)
            .WithOne(s => s.Package)
            .HasForeignKey<SenderAndRecipientDetails>(s => s.PackageRef);

        modelBuilder.Entity<PackageInformation>()
            .HasMany(p => p.TimeStampHistories)
            .WithOne(s => s.Package)
            .HasForeignKey(s => s.PackageRef);

        //Seeding database with dummy content.
        void AddDummyEntities(int id)
        {

            PackageInformation package = new PackageInformation()
            {
                Id = id,
            };

            modelBuilder.Entity<PackageInformation>().HasData(package);

            SenderAndRecipientDetails packageDetails = new SenderAndRecipientDetails()
            {
                Id = id,
                SenderFirstName = "John",
                SenderLastName = "Doe",
                SenderAddress = "Somest101",
                SenderPhone = "888888888",
                RecipientFirstName = "Some",
                RecipientLastName = "Guy",
                RecipientAddress = "Otherst010",
                RecipientPhone = "123456789",
                PackageRef = package.Id,
            };

            modelBuilder.Entity<SenderAndRecipientDetails>().HasData(packageDetails);


            StatusHistory history1 = new StatusHistory()
            {
                Id = id,
                PackageRef = package.Id,
                Status = 0,
                DisplayDate = Convert.ToBoolean(_configuration?["UseInMemoryDatabase"]) ? DateTime.Now : DateTime.Parse("2026-01-01"), //For seeding without InMemoryDatabase setting, hardcoded value is needed for this property.
            };

            modelBuilder.Entity<StatusHistory>().HasData(history1);
        }

        for (int i = 1; i <= 10; i++)
        {
            AddDummyEntities(i);
        }

    }

    public virtual DbSet<PackageInformation> PackageInformations { get; set; }
    public virtual DbSet<StatusHistory> StatusHistories { get; set; }


}
