using BikeRental.Domain.Enums;

namespace BikeRental.Domain;

/// <summary>
/// Represents the bicycle model specification.
/// </summary>
public class Model
{
    /// <summary>
    /// Unique identifier for the bicycle model.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name or title of the model.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Type of the bicycle (city, sport, mountain, etc.).
    /// </summary>
    public BikeType Type { get; set; }

    /// <summary>
    /// Wheel size of the bicycle in inches.
    /// </summary>
    public double WheelSize { get; set; }

    /// <summary>
    /// Maximum supported rider weight in kilograms.
    /// </summary>
    public double MaxWeight { get; set; }

    /// <summary>
    /// Weight of the bicycle itself in kilograms.
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Type of brake system installed on the bicycle.
    /// </summary>
    public BrakeType Brakes { get; set; } 

    /// <summary>
    /// Year when this model was manufactured or introduced.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Price for one hour of bicycle rental.
    /// </summary>
    public decimal PricePerHour { get; set; }
}
