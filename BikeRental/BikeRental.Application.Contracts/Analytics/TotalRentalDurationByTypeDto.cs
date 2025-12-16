using BikeRental.Domain.Enums;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing total rental duration aggregated by bicycle type.
/// </summary>
/// <param name="Type">Type of the bicycle model.</param>
/// <param name="TotalDurationHours">Total duration of rentals (in hours) for the specified type.</param>
public sealed record TotalRentalDurationByTypeDto(
    BikeType Type,
    int TotalDurationHours);