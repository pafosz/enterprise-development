using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.Bicycles;

namespace BikeRental.Application.Contracts;

/// <summary>
/// Defines an application-level contract for analytics queries over the bike rental domain.
/// Intended to be exposed via a dedicated controller.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Gets all sports bicycles.
    /// </summary>
    /// <returns>A list of bicycles classified as sports bicycles.</returns>
    public Task<IList<BicycleDto>> GetAllSportsBicycles();

    /// <summary>
    /// Gets the top bicycle models by total rental profit.
    /// </summary>
    /// <param name="count">The maximum number of results to return.</param>
    /// <returns>A list of model results ordered by total profit in descending order.</returns>
    public Task<IList<TopModelByProfitDto>> GetTopModelsByProfit(int count = 5);

    /// <summary>
    /// Gets the top bicycle models by total rental duration.
    /// </summary>
    /// <param name="count">The maximum number of results to return.</param>
    /// <returns>A list of model results ordered by total rental duration in descending order.</returns>
    public Task<IList<TopModelByDurationDto>> GetTopModelsByDuration(int count = 5);

    /// <summary>
    /// Gets the minimum, maximum, and average rental duration across all rentals.
    /// </summary>
    /// <returns>Aggregate statistics for rental duration.</returns>
    public Task<RentalDurationStatsDto> GetRentalDurationStats();

    /// <summary>
    /// Gets the total rental duration aggregated by bicycle type.
    /// </summary>
    /// <returns>A list of totals grouped by bicycle type.</returns>
    public Task<IList<TotalRentalDurationByTypeDto>> GetTotalRentalDurationByType();

    /// <summary>
    /// Gets renters who have rented bicycles the highest number of times.
    /// </summary>
    /// <param name="count">The maximum number of renters to return.</param>
    /// <returns>A list of renters ordered by rentals count in descending order.</returns>
    public Task<IList<TopRenterByRentalsCountDto>> GetTopRentersByRentalsCount(int count = 5);
}
