namespace BikeRental.Application.Contracts.Bicycles;

/// <summary>
/// Data transfer object used to create or update a bicycle.
/// </summary>
/// <param name="SerialNumber">Serial number printed on the bicycle frame.</param>
/// <param name="Color">Color of the bicycle.</param>
/// <param name="ModelId">Unique identifier of the model to which this bicycle belongs.</param>
public sealed record BicycleCreateUpdateDto(
    string SerialNumber,
    string Color,
    int ModelId);