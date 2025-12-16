namespace BikeRental.Application.Contracts.Bicycles;

/// <summary>
/// Data transfer object representing a bicycle.
/// </summary>
/// <param name="Id">Unique identifier of the bicycle.</param>
/// <param name="SerialNumber">Serial number printed on the bicycle frame.</param>
/// <param name="Color">Color of the bicycle.</param>
/// <param name="ModelId">Unique identifier of the model to which this bicycle belongs.</param>
public sealed record BicycleDto(
    int Id,
    string SerialNumber,
    string Color,
    int ModelId);