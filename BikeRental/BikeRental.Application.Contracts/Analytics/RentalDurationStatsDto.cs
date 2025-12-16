namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing aggregate statistics for rental duration.
/// </summary>
/// <param name="MinDurationHours">Minimum rental duration in hours.</param>
/// <param name="MaxDurationHours">Maximum rental duration in hours.</param>
/// <param name="AverageDurationHours">Average rental duration in hours.</param>
public sealed record RentalDurationStatsDto(
    int MinDurationHours,
    int MaxDurationHours,
    double AverageDurationHours);