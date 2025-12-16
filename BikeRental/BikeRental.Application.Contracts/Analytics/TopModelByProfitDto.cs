using BikeRental.Application.Contracts.Models;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing a bicycle model result ordered by total profit.
/// </summary>
/// <param name="Model">Bicycle model information.</param>
/// <param name="TotalProfit">Total profit accumulated from rentals of the specified model.</param>
public sealed record TopModelByProfitDto(
    ModelDto Model,
    decimal TotalProfit);