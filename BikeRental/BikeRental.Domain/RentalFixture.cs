using BikeRental.Domain;
using BikeRental.Domain.Enums;

namespace BikeRental.Domain;

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
        Models =
        [
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
        ];

        // --- Bicycles ---
        Bicycles =
        [
            new() { Id = 1,  SerialNumber = "SN1001", Color = "Red", ModelId = Models[0].Id, Model = Models[0] },
            new() { Id = 2,  SerialNumber = "SN1002", Color = "Blue", ModelId = Models[1].Id, Model = Models[1] },
            new() { Id = 3,  SerialNumber = "SN1003", Color = "Green", ModelId = Models[2].Id, Model = Models[2] },
            new() { Id = 4,  SerialNumber = "SN1004", Color = "Yellow", ModelId = Models[3].Id, Model = Models[3] },
            new() { Id = 5,  SerialNumber = "SN1005", Color = "Black", ModelId = Models[4].Id, Model = Models[4] },
            new() { Id = 6,  SerialNumber = "SN1006", Color = "Silver", ModelId = Models[5].Id, Model = Models[5] },
            new() { Id = 7,  SerialNumber = "SN1007", Color = "White", ModelId = Models[6].Id, Model = Models[6] },
            new() { Id = 8,  SerialNumber = "SN1008", Color = "Gray", ModelId = Models[7].Id, Model = Models[7] },
            new() { Id = 9,  SerialNumber = "SN1009", Color = "Orange", ModelId = Models[8].Id, Model = Models[8] },
            new() { Id = 10, SerialNumber = "SN1010", Color = "Blue", ModelId = Models[9].Id, Model = Models[9] },
            new() { Id = 11, SerialNumber = "SN1011", Color = "Purple", ModelId = Models[0].Id, Model = Models[0] },
            new() { Id = 12, SerialNumber = "SN1012", Color = "Brown", ModelId = Models[2].Id, Model = Models[2] }
        ];

        // --- Renters ---
        Renters =
        [
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
        ];

        // --- Rentals ---
        Rentals =
        [
            new() { Id = 1, BicycleId = Bicycles[0].Id, Bicycle = Bicycles[0], RenterId = Renters[0].Id, Renter = Renters[0], StartTime = DateTime.Now.AddHours(-12), DurationHours = 3 },
            new() { Id = 2, BicycleId = Bicycles[1].Id, Bicycle = Bicycles[1], RenterId = Renters[1].Id, Renter = Renters[1], StartTime = DateTime.Now.AddHours(-10), DurationHours = 5 },
            new() { Id = 3, BicycleId = Bicycles[2].Id, Bicycle = Bicycles[2], RenterId = Renters[2].Id, Renter = Renters[2], StartTime = DateTime.Now.AddHours(-8), DurationHours = 2 },
            new() { Id = 4, BicycleId = Bicycles[3].Id, Bicycle = Bicycles[3], RenterId = Renters[3].Id, Renter = Renters[3], StartTime = DateTime.Now.AddHours(-6), DurationHours = 6 },
            new() { Id = 5, BicycleId = Bicycles[4].Id, Bicycle = Bicycles[4], RenterId = Renters[4].Id, Renter = Renters[4], StartTime = DateTime.Now.AddHours(-4), DurationHours = 4 },
            new() { Id = 6, BicycleId = Bicycles[5].Id, Bicycle = Bicycles[5], RenterId = Renters[5].Id, Renter = Renters[5], StartTime = DateTime.Now.AddHours(-2), DurationHours = 7 },
            new() { Id = 7, BicycleId = Bicycles[6].Id, Bicycle = Bicycles[6], RenterId = Renters[6].Id, Renter = Renters[6], StartTime = DateTime.Now.AddHours(-1), DurationHours = 5 },
            new() { Id = 8, BicycleId = Bicycles[7].Id, Bicycle = Bicycles[7], RenterId = Renters[7].Id, Renter = Renters[7], StartTime = DateTime.Now.AddHours(-3), DurationHours = 3 },
            new() { Id = 9, BicycleId = Bicycles[8].Id, Bicycle = Bicycles[8], RenterId = Renters[8].Id, Renter = Renters[8], StartTime = DateTime.Now.AddHours(-5), DurationHours = 4 },
            new() { Id = 10, BicycleId = Bicycles[9].Id, Bicycle = Bicycles[9], RenterId = Renters[9].Id, Renter = Renters[9], StartTime = DateTime.Now.AddHours(-7), DurationHours = 6 },
            new() { Id = 11, BicycleId = Bicycles[1].Id, Bicycle = Bicycles[1], RenterId = Renters[2].Id, Renter = Renters[2], StartTime = DateTime.Now.AddHours(-15), DurationHours = 3 },
            new() { Id = 12, BicycleId = Bicycles[2].Id, Bicycle = Bicycles[2], RenterId = Renters[4].Id, Renter = Renters[4], StartTime = DateTime.Now.AddHours(-20), DurationHours = 4 },
            new() { Id = 13, BicycleId = Bicycles[5].Id, Bicycle = Bicycles[5], RenterId = Renters[0].Id, Renter = Renters[0], StartTime = DateTime.Now.AddHours(-30), DurationHours = 2 },
            new() { Id = 14, BicycleId = Bicycles[7].Id, Bicycle = Bicycles[7], RenterId = Renters[3].Id, Renter = Renters[3], StartTime = DateTime.Now.AddHours(-25), DurationHours = 5 },
            new() { Id = 15, BicycleId = Bicycles[9].Id, Bicycle = Bicycles[9], RenterId = Renters[6].Id, Renter = Renters[6], StartTime = DateTime.Now.AddHours(-50), DurationHours = 3 },
            new() { Id = 16, BicycleId = Bicycles[0].Id, Bicycle = Bicycles[0], RenterId = Renters[1].Id, Renter = Renters[1], StartTime = DateTime.Now.AddHours(-35), DurationHours = 2 },
            new() { Id = 17, BicycleId = Bicycles[4].Id, Bicycle = Bicycles[4], RenterId = Renters[9].Id, Renter = Renters[9], StartTime = DateTime.Now.AddHours(-40), DurationHours = 6 },
            new() { Id = 18, BicycleId = Bicycles[3].Id, Bicycle = Bicycles[3], RenterId = Renters[5].Id, Renter = Renters[5], StartTime = DateTime.Now.AddHours(-18), DurationHours = 7 },
            new() { Id = 19, BicycleId = Bicycles[8].Id, Bicycle = Bicycles[8], RenterId = Renters[8].Id, Renter = Renters[8], StartTime = DateTime.Now.AddHours(-28), DurationHours = 5 },
            new() { Id = 20, BicycleId = Bicycles[6].Id, Bicycle = Bicycles[6], RenterId = Renters[2].Id, Renter = Renters[2], StartTime = DateTime.Now.AddHours(-16), DurationHours = 3 },
            new() { Id = 21, BicycleId = Bicycles[5].Id, Bicycle = Bicycles[5], RenterId = Renters[0].Id, Renter = Renters[0], StartTime = DateTime.Now.AddDays(-3).AddHours(-4), DurationHours = 6 },
            new() { Id = 22, BicycleId = Bicycles[2].Id, Bicycle = Bicycles[2], RenterId = Renters[4].Id, Renter = Renters[4], StartTime = DateTime.Now.AddDays(-2).AddHours(-2), DurationHours = 4 },
            new() { Id = 23, BicycleId = Bicycles[7].Id, Bicycle = Bicycles[7], RenterId = Renters[7].Id, Renter = Renters[7], StartTime = DateTime.Now.AddDays(-1).AddHours(-1), DurationHours = 2 },
            new() { Id = 24, BicycleId = Bicycles[1].Id, Bicycle = Bicycles[1], RenterId = Renters[3].Id, Renter = Renters[3], StartTime = DateTime.Now.AddDays(-1).AddHours(-5), DurationHours = 5 },
            new() { Id = 25, BicycleId = Bicycles[8].Id, Bicycle = Bicycles[8], RenterId = Renters[8].Id, Renter = Renters[8], StartTime = DateTime.Now.AddDays(-2).AddHours(-3), DurationHours = 7 },
            new() { Id = 26, BicycleId = Bicycles[0].Id, Bicycle = Bicycles[0], RenterId = Renters[2].Id, Renter = Renters[2], StartTime = DateTime.Now.AddDays(-4).AddHours(-6), DurationHours = 2 },
            new() { Id = 27, BicycleId = Bicycles[5].Id, Bicycle = Bicycles[5], RenterId = Renters[6].Id, Renter = Renters[6], StartTime = DateTime.Now.AddDays(-5).AddHours(-3), DurationHours = 5 },
            new() { Id = 28, BicycleId = Bicycles[9].Id, Bicycle = Bicycles[9], RenterId = Renters[4].Id, Renter = Renters[4], StartTime = DateTime.Now.AddDays(-6).AddHours(-2), DurationHours = 3 },
            new() { Id = 29, BicycleId = Bicycles[2].Id, Bicycle = Bicycles[2], RenterId = Renters[9].Id, Renter = Renters[9], StartTime = DateTime.Now.AddDays(-7).AddHours(-1), DurationHours = 4 },
            new() { Id = 30, BicycleId = Bicycles[6].Id, Bicycle = Bicycles[6], RenterId = Renters[5].Id, Renter = Renters[5], StartTime = DateTime.Now.AddDays(-8).AddHours(-2), DurationHours = 6 },
            new() { Id = 31, BicycleId = Bicycles[5].Id, Bicycle = Bicycles[5], RenterId = Renters[3].Id, Renter = Renters[3], StartTime = DateTime.Now.AddDays(-10).AddHours(-5), DurationHours = 5 },
            new() { Id = 32, BicycleId = Bicycles[9].Id, Bicycle = Bicycles[9], RenterId = Renters[0].Id, Renter = Renters[0], StartTime = DateTime.Now.AddDays(-12).AddHours(-3), DurationHours = 4 },
            new() { Id = 33, BicycleId = Bicycles[4].Id, Bicycle = Bicycles[4], RenterId = Renters[8].Id, Renter = Renters[8], StartTime = DateTime.Now.AddDays(-15).AddHours(-6), DurationHours = 7 },
            new() { Id = 34, BicycleId = Bicycles[6].Id, Bicycle = Bicycles[6], RenterId = Renters[7].Id, Renter = Renters[7], StartTime = DateTime.Now.AddDays(-18).AddHours(-4), DurationHours = 5 },
            new() { Id = 35, BicycleId = Bicycles[2].Id, Bicycle = Bicycles[2], RenterId = Renters[1].Id, Renter = Renters[1], StartTime = DateTime.Now.AddDays(-20).AddHours(-3), DurationHours = 3 }
        ];
    }
}
