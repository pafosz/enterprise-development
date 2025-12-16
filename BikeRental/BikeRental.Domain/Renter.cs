namespace BikeRental.Domain;
/// <summary>
/// Represents a person who rents bicycles.
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier for the renter.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number of the renter.
    /// </summary>
    public string? Phone {  get; set; }
}
