using BikeRental.Domain;
using BikeRental.Domain.Enums;

namespace BikeRental.Tests;

/// <summary>
/// Provides preloaded in-memory data for testing purposes.
/// This fixture replaces the static DataSeed and is created once per test class.
/// </summary>
public class RentalFixture
{
    /// <summary>
    /// List of available bicycle models in the rental system.
    /// </summary>
    public List<Model> Models { get; }

    /// <summary>
    /// List of bicycles available for rent.
    /// </summary>
    public List<Bicycle> Bicycles { get; }

    /// <summary>
    /// List of renters registered in the system.
    /// </summary>
    public List<Renter> Renters { get; }

    /// <summary>
    /// List of rental records.
    /// </summary>
    public List<Rental> Rentals { get; }

    /// <summary>
    /// Initializes the in-memory test data. 
    /// Fully identical to the static DataSeed class, but instance-based for fixture use.
    /// </summary>
    public RentalFixture()
    {
        // --- Models ---
        Models = new List<Model>
        {
            new() { Id = 1, Name = "Stels Navigator", Type = BikeType.Urban, WheelSize = 26, MaxWeight = 120, Weight = 15, Brakes = BrakeType.Disc, Year = 2022, PricePerHour = 150 },
            new() { Id = 2, Name = "Cube Race", Type = BikeType.Sports, WheelSize = 28, MaxWeight = 100, Weight = 12, Brakes = BrakeType.Rim, Year = 2021, PricePerHour = 250 },
            new() { Id = 3, Name = "Merida Speed", Type = BikeType.Sports, WheelSize = 29, MaxWeight = 110, Weight = 13, Brakes = BrakeType.Disc, Year = 2023, PricePerHour = 300 },
            new() { Id = 4, Name = "Forward Junior", Type = BikeType.Childrens, WheelSize = 20, MaxWeight = 50, Weight = 8, Brakes = BrakeType.Rim, Year = 2020, PricePerHour = 100 },
            new() { Id = 5, Name = "Trek FX", Type = BikeType.Urban, WheelSize = 27.5, MaxWeight = 130, Weight = 14, Brakes = BrakeType.Disc, Year = 2022, PricePerHour = 200 },
            new() { Id = 6, Name = "Giant Sport", Type = BikeType.Sports, WheelSize = 29, MaxWeight = 120, Weight = 14, Brakes = BrakeType.Disc, Year = 2023, PricePerHour = 280 },
            new() { Id = 7, Name = "Stark Cobra", Type = BikeType.Mountain, WheelSize = 27.5, MaxWeight = 110, Weight = 13, Brakes = BrakeType.Disc, Year = 2022, PricePerHour = 220 },
            new() { Id = 8, Name = "Scott City", Type = BikeType.Urban, WheelSize = 28, MaxWeight = 100, Weight = 12, Brakes = BrakeType.Rim, Year = 2023, PricePerHour = 180 },
            new() { Id = 9, Name = "Author MTB", Type = BikeType.Mountain, WheelSize = 29, MaxWeight = 115, Weight = 14, Brakes = BrakeType.Disc, Year = 2024, PricePerHour = 260 },
            new() { Id = 10, Name = "Orbea Aero", Type = BikeType.Sports, WheelSize = 28, MaxWeight = 100, Weight = 11, Brakes = BrakeType.Disc, Year = 2023, PricePerHour = 320 }
        };

        // --- Bicycles ---
        Bicycles = new List<Bicycle>
        {
            new() { Id = 1, SerialNumber = "SN1001", Color = "Red", Model = Models[0] },
            new() { Id = 2, SerialNumber = "SN1002", Color = "Blue", Model = Models[1] },
            new() { Id = 3, SerialNumber = "SN1003", Color = "Green", Model = Models[2] },
            new() { Id = 4, SerialNumber = "SN1004", Color = "Yellow", Model = Models[3] },
            new() { Id = 5, SerialNumber = "SN1005", Color = "Black", Model = Models[4] },
            new() { Id = 6, SerialNumber = "SN1006", Color = "Silver", Model = Models[5] },
            new() { Id = 7, SerialNumber = "SN1007", Color = "White", Model = Models[6] },
            new() { Id = 8, SerialNumber = "SN1008", Color = "Gray", Model = Models[7] },
            new() { Id = 9, SerialNumber = "SN1009", Color = "Orange", Model = Models[8] },
            new() { Id = 10, SerialNumber = "SN1010", Color = "Blue", Model = Models[9] }
        };

        // --- Renters ---
        Renters = new List<Renter>
        {
            new() { Id = 1, FullName = "John Smith", Phone = "+1-202-111-22-33" },
            new() { Id = 2, FullName = "Mary Johnson", Phone = "+1-202-222-33-44" },
            new() { Id = 3, FullName = "Robert Brown", Phone = "+1-202-333-44-55" },
            new() { Id = 4, FullName = "Jennifer Davis", Phone = "+1-202-444-55-66" },
            new() { Id = 5, FullName = "Michael Miller", Phone = "+1-202-555-66-77" },
            new() { Id = 6, FullName = "William Wilson", Phone = "+1-202-666-77-88" },
            new() { Id = 7, FullName = "Elizabeth Moore", Phone = "+1-202-777-88-99" },
            new() { Id = 8, FullName = "David Taylor", Phone = "+1-202-888-99-00" },
            new() { Id = 9, FullName = "Linda Anderson", Phone = "+1-202-999-00-11" },
            new() { Id = 10, FullName = "James Thomas", Phone = "+1-202-000-11-22" }
        };

        // --- Rentals ---
        Rentals = new List<Rental>
        {
            new() { Id = 1, Bicycle = Bicycles[0], Renter = Renters[0], StartTime = DateTime.Now.AddHours(-12), DurationHours = 3 },
            new() { Id = 2, Bicycle = Bicycles[1], Renter = Renters[1], StartTime = DateTime.Now.AddHours(-10), DurationHours = 5 },
            new() { Id = 3, Bicycle = Bicycles[2], Renter = Renters[2], StartTime = DateTime.Now.AddHours(-8), DurationHours = 2 },
            new() { Id = 4, Bicycle = Bicycles[3], Renter = Renters[3], StartTime = DateTime.Now.AddHours(-6), DurationHours = 6 },
            new() { Id = 5, Bicycle = Bicycles[4], Renter = Renters[4], StartTime = DateTime.Now.AddHours(-4), DurationHours = 4 },
            new() { Id = 6, Bicycle = Bicycles[5], Renter = Renters[5], StartTime = DateTime.Now.AddHours(-2), DurationHours = 7 },
            new() { Id = 7, Bicycle = Bicycles[6], Renter = Renters[6], StartTime = DateTime.Now.AddHours(-1), DurationHours = 5 },
            new() { Id = 8, Bicycle = Bicycles[7], Renter = Renters[7], StartTime = DateTime.Now.AddHours(-3), DurationHours = 3 },
            new() { Id = 9, Bicycle = Bicycles[8], Renter = Renters[8], StartTime = DateTime.Now.AddHours(-5), DurationHours = 4 },
            new() { Id = 10, Bicycle = Bicycles[9], Renter = Renters[9], StartTime = DateTime.Now.AddHours(-7), DurationHours = 6 }
        };
    }
}
