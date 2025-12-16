using BikeRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore;

/// <summary>
/// Entity Framework Core database context for the BikeRental application.
/// </summary>
/// <param name="options">EF Core context options (provider, connection string, etc.).</param>
/// <param name="fixture">
/// In-memory fixture used as a source for deterministic seed data.
/// Seed data is applied via <see cref="ModelBuilder"/> using scalar properties and FK identifiers only.
/// </param>
public class BikeRentalDbContext(DbContextOptions<BikeRentalDbContext> options, RentalFixture fixture) : DbContext(options)
{
    /// <summary>
    /// Gets the set of renters persisted in the database.
    /// </summary>
    public DbSet<Renter> Renters => Set<Renter>();

    /// <summary>
    /// Gets the set of bicycles persisted in the database.
    /// </summary>
    public DbSet<Bicycle> Bicycles => Set<Bicycle>();

    /// <summary>
    /// Gets the set of bicycle models persisted in the database.
    /// </summary>
    public DbSet<Model> Models => Set<Model>();

    /// <summary>
    /// Gets the set of rental records persisted in the database.
    /// </summary>
    public DbSet<Rental> Rentals => Set<Rental>();

    /// <summary>
    /// Configures EF Core mappings, constraints, indexes and seed data for SQL Server.
    /// Seed data is applied with explicit identifiers (Id) and required foreign keys (FKs).
    /// Navigation properties are not used for seeding.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the EF Core model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("Models");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.Property(e => e.Type)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.Brakes)
                .HasConversion<int>();

            entity.Property(e => e.WheelSize).HasColumnType("float");
            entity.Property(e => e.MaxWeight).IsRequired().HasColumnType("float");
            entity.Property(e => e.Weight).HasColumnType("float");

            entity.Property(e => e.Year).HasColumnType("int");

            entity.Property(e => e.PricePerHour)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Bicycle>(entity =>
        {
            entity.ToTable("Bicycles");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.SerialNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.ModelId)
                .IsRequired();

            entity.HasOne(e => e.Model)
                .WithMany()
                .HasForeignKey(e => e.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.ModelId);
        });

        modelBuilder.Entity<Renter>(entity =>
        {
            entity.ToTable("Renters");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Phone)
                .HasMaxLength(32);
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.ToTable("Rentals");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnType("datetime2");

            entity.Property(e => e.DurationHours)
                .IsRequired();

            entity.Property(e => e.BicycleId)
                .IsRequired();

            entity.Property(e => e.RenterId)
                .IsRequired();

            entity.HasOne(e => e.Bicycle)
                .WithMany()
                .HasForeignKey(e => e.BicycleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Renter)
                .WithMany()
                .HasForeignKey(e => e.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.BicycleId);
            entity.HasIndex(e => e.RenterId);

            entity.Ignore(e => e.TotalPrice);
        });

        modelBuilder.Entity<Model>().HasData(
            fixture.Models.Select(m => new Model
            {
                Id = m.Id,
                Name = m.Name,
                Type = m.Type,
                WheelSize = m.WheelSize,
                MaxWeight = m.MaxWeight,
                Weight = m.Weight,
                Brakes = m.Brakes,
                Year = m.Year,
                PricePerHour = m.PricePerHour
            })
        );

        modelBuilder.Entity<Bicycle>().HasData(
            fixture.Bicycles.Select(b => new Bicycle
            {
                Id = b.Id,
                SerialNumber = b.SerialNumber,
                Color = b.Color,
                ModelId = b.ModelId
            })
        );

        modelBuilder.Entity<Renter>().HasData(
            fixture.Renters.Select(r => new Renter
            {
                Id = r.Id,
                FullName = r.FullName,
                Phone = r.Phone
            })
        );

        modelBuilder.Entity<Rental>().HasData(
            fixture.Rentals.Select(r => new Rental
            {
                Id = r.Id,
                BicycleId = r.BicycleId,
                RenterId = r.RenterId,
                StartTime = r.StartTime,
                DurationHours = r.DurationHours
            })
        );

        base.OnModelCreating(modelBuilder);
    }
}
