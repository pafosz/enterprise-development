namespace BikeRental.Domain;
/// <summary>
/// Represents a physical bicycle unit available for rental.
/// </summary>
public class Bicycle
{
    /// <summary>
    /// Unique identifier for the bicycle.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Serial number printed on the bicycle frame.
    /// </summary>
    public required string SerialNumber { get; set; } 

    /// <summary>
    /// Color of the bicycle.
    /// </summary>
    public required string Color {  get; set; }

    /// <summary>
    /// Unique identifier of the model to which this bicycle belongs.
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// The model to which this bicycle belongs.
    /// </summary>
    public Model? Model { get; set; }
}
