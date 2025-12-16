using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Domain;
using BikeRental.Domain.Enums;
using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.Bicycles;
using BikeRental.Application.Contracts.Models;
using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides application-level analytics queries over the bike rental domain.
/// All computations are performed using data retrieved through repositories.
/// </summary>
/// <param name="rentalRepository">Repository used to access rental data.</param>
/// <param name="bicycleRepository">Repository used to access bicycle data.</param>
/// <param name="modelRepository">Repository used to access model data.</param>
/// <param name="renterRepository">Repository used to access renter data.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class AnalyticsService(
    IRepository<Rental, int> rentalRepository,
    IRepository<Bicycle, int> bicycleRepository,
    IRepository<Model, int> modelRepository,
    IRepository<Renter, int> renterRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Gets all sports bicycles.
    /// </summary>
    /// <returns>A list of bicycles classified as sports bicycles.</returns>
    public async Task<IList<BicycleDto>> GetAllSportsBicycles()
    {
        var models = await modelRepository.ReadAll();
        var bicycles = await bicycleRepository.ReadAll();

        var sportsModelIds = models
            .Where(m => m.Type == BikeType.Sports)
            .Select(m => m.Id)
            .ToHashSet();

        return [.. bicycles
            .Where(b => sportsModelIds.Contains(b.ModelId))
            .Select(mapper.Map<BicycleDto>)];
    }

    /// <summary>
    /// Gets the top bicycle models by total rental profit.
    /// </summary>
    /// <param name="count">The maximum number of results to return.</param>
    /// <returns>A list of model results ordered by total profit in descending order.</returns>
    public async Task<IList<TopModelByProfitDto>> GetTopModelsByProfit(int count = 5)
    {
        var rentals = await rentalRepository.ReadAll();
        var bicycles = await bicycleRepository.ReadAll();
        var models = await modelRepository.ReadAll();

        var bicycleById = bicycles.ToDictionary(b => b.Id);
        var modelById = models.ToDictionary(m => m.Id);

        var profitByModelId = new Dictionary<int, decimal>();

        foreach (var rental in rentals)
        {
            if (!bicycleById.TryGetValue(rental.BicycleId, out var bicycle))
                continue;

            if (!modelById.TryGetValue(bicycle.ModelId, out var model))
                continue;

            var profit = model.PricePerHour * rental.DurationHours;

            if (profitByModelId.TryGetValue(model.Id, out var current))
                profitByModelId[model.Id] = current + profit;
            else
                profitByModelId[model.Id] = profit;
        }

        return [.. profitByModelId
            .OrderByDescending(x => x.Value)
            .Take(Math.Max(0, count))
            .Select(x => new TopModelByProfitDto(
                mapper.Map<ModelDto>(modelById[x.Key]),
                x.Value))];
    }

    /// <summary>
    /// Gets the top bicycle models by total rental duration.
    /// </summary>
    /// <param name="count">The maximum number of results to return.</param>
    /// <returns>A list of model results ordered by total rental duration in descending order.</returns>
    public async Task<IList<TopModelByDurationDto>> GetTopModelsByDuration(int count = 5)
    {
        var rentals = await rentalRepository.ReadAll();
        var bicycles = await bicycleRepository.ReadAll();
        var models = await modelRepository.ReadAll();

        var bicycleById = bicycles.ToDictionary(b => b.Id);
        var modelById = models.ToDictionary(m => m.Id);

        var durationByModelId = new Dictionary<int, int>();

        foreach (var rental in rentals)
        {
            if (!bicycleById.TryGetValue(rental.BicycleId, out var bicycle))
                continue;

            if (!modelById.ContainsKey(bicycle.ModelId))
                continue;

            if (durationByModelId.TryGetValue(bicycle.ModelId, out var current))
                durationByModelId[bicycle.ModelId] = current + rental.DurationHours;
            else
                durationByModelId[bicycle.ModelId] = rental.DurationHours;
        }

        return [.. durationByModelId
            .OrderByDescending(x => x.Value)
            .Take(Math.Max(0, count))
            .Select(x => new TopModelByDurationDto(
                mapper.Map<ModelDto>(modelById[x.Key]),
                x.Value))];
    }

    /// <summary>
    /// Gets the minimum, maximum, and average rental duration across all rentals.
    /// </summary>
    /// <returns>Aggregate statistics for rental duration.</returns>
    public async Task<RentalDurationStatsDto> GetRentalDurationStats()
    {
        var rentals = await rentalRepository.ReadAll();
        if (rentals.Count == 0)
            return new RentalDurationStatsDto(0, 0, 0d);

        var min = rentals.Min(r => r.DurationHours);
        var max = rentals.Max(r => r.DurationHours);
        var avg = rentals.Average(r => r.DurationHours);

        return new RentalDurationStatsDto(min, max, avg);
    }

    /// <summary>
    /// Gets the total rental duration aggregated by bicycle type.
    /// </summary>
    /// <returns>A list of totals grouped by bicycle type.</returns>
    public async Task<IList<TotalRentalDurationByTypeDto>> GetTotalRentalDurationByType()
    {
        var rentals = await rentalRepository.ReadAll();
        var bicycles = await bicycleRepository.ReadAll();
        var models = await modelRepository.ReadAll();

        var bicycleById = bicycles.ToDictionary(b => b.Id);
        var modelById = models.ToDictionary(m => m.Id);

        var durationByType = new Dictionary<BikeType, int>();

        foreach (var rental in rentals)
        {
            if (!bicycleById.TryGetValue(rental.BicycleId, out var bicycle))
                continue;

            if (!modelById.TryGetValue(bicycle.ModelId, out var model))
                continue;

            if (durationByType.TryGetValue(model.Type, out var current))
                durationByType[model.Type] = current + rental.DurationHours;
            else
                durationByType[model.Type] = rental.DurationHours;
        }

        return [.. durationByType
            .OrderBy(x => x.Key)
            .Select(x => new TotalRentalDurationByTypeDto(x.Key, x.Value))];
    }

    /// <summary>
    /// Gets renters who have rented bicycles the highest number of times.
    /// </summary>
    /// <param name="count">The maximum number of renters to return.</param>
    /// <returns>A list of renters ordered by rentals count in descending order.</returns>
    public async Task<IList<TopRenterByRentalsCountDto>> GetTopRentersByRentalsCount(int count = 5)
    {
        var rentals = await rentalRepository.ReadAll();
        var renters = await renterRepository.ReadAll();

        var renterById = renters.ToDictionary(r => r.Id);

        var rentalsCountByRenterId = new Dictionary<int, int>();
        foreach (var rental in rentals)
        {
            if (!renterById.ContainsKey(rental.RenterId))
                continue;

            if (rentalsCountByRenterId.TryGetValue(rental.RenterId, out var current))
                rentalsCountByRenterId[rental.RenterId] = current + 1;
            else
                rentalsCountByRenterId[rental.RenterId] = 1;
        }

        return [.. rentalsCountByRenterId
            .OrderByDescending(x => x.Value)
            .Take(Math.Max(0, count))
            .Select(x => new TopRenterByRentalsCountDto(
                mapper.Map<RenterDto>(renterById[x.Key]),
                x.Value))];
    }
}
