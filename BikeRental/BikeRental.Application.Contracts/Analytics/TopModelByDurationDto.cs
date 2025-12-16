using BikeRental.Application.Contracts.Models;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing a bicycle model result ordered by total rental duration.
/// </summary>
/// <param name="Model">Bicycle model information.</param>
/// <param name="TotalDurationHours">Total duration of rentals (in hours) for the specified model.</param>
public sealed record TopModelByDurationDto(
    ModelDto Model,
    int TotalDurationHours);