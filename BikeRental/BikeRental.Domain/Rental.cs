namespace BikeRental.Domain;
/// <summary>
/// Represents a record of a bicycle rental made by a renter.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier for the rental record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The bicycle being rented.
    /// </summary>
    public required Bicycle Bicycle { get; set; } 

    /// <summary>
    /// The renter who rented the bicycle.
    /// </summary>
    public required Renter Renter { get; set; } 

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public required int DurationHours { get; set; }

    /// <summary>
    /// Calculated total rental price based on duration and model price per hour.
    /// </summary>
    public decimal TotalPrice => Bicycle.Model.PricePerHour * DurationHours;
}
