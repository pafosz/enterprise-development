namespace BikeRental;
/// <summary>
/// Represents a physical bicycle unit available for rental.
/// </summary>
public class Bicycle
{
    /// <summary>
    /// Unique identifier for the bicycle.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Serial number printed on the bicycle frame.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Color of the bicycle.
    /// </summary>
    public string Color {  get; set; } = string.Empty;

    /// <summary>
    /// The model to which this bicycle belongs.
    /// </summary>
    public Model Model { get; set; } = new Model(); // ссылка на модель
}
