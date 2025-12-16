using BikeRental.Domain.Enums;

namespace BikeRental.Application.Contracts.Models;

/// <summary>
/// Data transfer object used to create or update a bicycle model.
/// </summary>
/// <param name="Name">Name or title of the model.</param>
/// <param name="Type">Type of the bicycle (city, sport, mountain, etc.).</param>
/// <param name="WheelSize">Wheel size of the bicycle in inches.</param>
/// <param name="MaxWeight">Maximum supported rider weight in kilograms.</param>
/// <param name="Weight">Weight of the bicycle itself in kilograms.</param>
/// <param name="Brakes">Type of brake system installed on the bicycle.</param>
/// <param name="Year">Year when this model was manufactured or introduced.</param>
/// <param name="PricePerHour">Price for one hour of bicycle rental.</param>
public sealed record ModelCreateUpdateDto(
    string? Name,
    BikeType Type,
    double WheelSize,
    double MaxWeight,
    double Weight,
    BrakeType Brakes,
    int Year,
    decimal PricePerHour);