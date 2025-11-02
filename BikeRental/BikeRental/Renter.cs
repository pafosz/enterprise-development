namespace BikeRental.Domain;
/// <summary>
/// Represents a person who rents bicycles.
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier for the renter.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the renter.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Contact phone number of the renter.
    /// </summary>
    public string Phone {  get; set; } = string.Empty;
}
