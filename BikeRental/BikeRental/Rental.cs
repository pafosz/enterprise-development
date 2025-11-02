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
    public Bicycle Bicycle { get; set; } = new Bicycle();

    /// <summary>
    /// The renter who rented the bicycle.
    /// </summary>
    public Renter Renter { get; set; } = new Renter();

    /// <summary>
    /// Start time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }

    /// <summary>
    /// Calculated total rental price based on duration and model price per hour.
    /// </summary>
    public decimal TotalPrice => Bicycle.Model.PricePerHour * DurationHours;
}
