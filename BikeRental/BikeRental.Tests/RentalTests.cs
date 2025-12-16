using Xunit;
using BikeRental.Domain;
using BikeRental.Domain.Enums;

namespace BikeRental.Tests;

/// <summary>
/// Contains unit tests for the bike rental system.
/// Tests verify data queries, aggregation, and statistics using LINQ.
/// </summary>
public class RentalTests(RentalFixture fixture) : IClassFixture<RentalFixture>
{
     /// <summary>
    /// Checks that filtering bicycles by the "Sports" type
    /// correctly returns only sport bicycles and that
    /// the total count matches the expected number.
    /// </summary>
    [Fact]
    public void ShouldReturnAllSportBicycles()
    {
        var sportBikes = fixture.Bicycles
            .Where(b => b.Model!.Type == BikeType.Sports)
            .ToList();

        Assert.NotEmpty(sportBikes);
        Assert.True(sportBikes.All(b => b.Model!.Type == BikeType.Sports));
        Assert.Equal(5, sportBikes.Count);
    }

    /// <summary>
    /// Checks that the top five bicycle models are selected
    /// correctly when sorted by total rental profit.
    /// </summary>
    [Fact]
    public void ShouldReturnTop5ModelsByProfit()
    {
        var top = fixture.Rentals
            .GroupBy(r => r.Bicycle!.Model)
            .Select(g => new
            {
                Model = g.Key!.Name,
                Profit = g.Sum(r => r.TotalPrice)
            })
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .ToList();

        Assert.Equal(5, top.Count);
        Assert.True(top.Zip(top.Skip(1), (a, b) => a.Profit >= b.Profit).All(x => x));
    }

    /// <summary>
    /// Verifies that the top five bicycle models are returned
    /// correctly when sorted by total rental duration in hours.
    /// </summary>
    [Fact]
    public void ShouldReturnTop5ModelsByDuration()
    {
        var top = fixture.Rentals
            .GroupBy(r => r.Bicycle!.Model)
            .Select(g => new
            {
                Model = g.Key!.Name,
                TotalHours = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalHours)
            .Take(5)
            .ToList();

        Assert.Equal(5, top.Count);
    }

    /// <summary>
    /// Validates calculation of minimum, maximum,
    /// and average rental durations based on sample data.
    /// </summary>
    [Fact]
    public void ShouldReturnRentalDurationStats()
    {
        var durations = fixture.Rentals.Select(r => r.DurationHours).ToList();
        var min = durations.Min();
        var max = durations.Max();
        var avg = Math.Round(durations.Average(), 2);

        Assert.NotEmpty(durations);
        Assert.Equal(2, min);
        Assert.Equal(7, max);
        Assert.InRange(avg, 4.2, 4.5);
    }

    /// <summary>
    /// Checks that total rental time is correctly calculated
    /// for each bicycle type (sports, urban, mountain, childrens).
    /// </summary>
    [Fact]
    public void ShouldReturnTotalRentalTimeByBikeType()
    {
        var byType = fixture.Rentals
            .GroupBy(r => r.Bicycle!.Model!.Type)
            .Select(g => new { Type = g.Key, TotalHours = g.Sum(r => r.DurationHours) })
            .ToDictionary(x => x.Type, x => x.TotalHours);

        Assert.Equal(71, byType[BikeType.Sports]);
        Assert.Equal(34, byType[BikeType.Urban]);
        Assert.Equal(35, byType[BikeType.Mountain]);
        Assert.Equal(13, byType[BikeType.Childrens]);
    }

    /// <summary>
    /// Ensures that the list of renters is correctly aggregated
    /// by rental count and identifies the most active renters.
    /// </summary>
    [Fact]
    public void ShouldReturnTopRentersByCount()
    {
        var stats = fixture.Rentals
            .GroupBy(r => r.Renter)
            .Select(g => new { Renter = g.Key!.FullName, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        Assert.NotEmpty(stats);
        var max = stats.Max(x => x.Count);
        Assert.Equal(4, max);
    }

    /// <summary>
    /// Checks that bicycles which were never rented are correctly identified.
    /// </summary>
    [Fact]
    public void ShouldReturnUnrentedBicycles()
    {
        var rentedIds = fixture.Rentals.
            Select(r => r.Bicycle!.Id)
            .Distinct()
            .ToHashSet();

        var unrented = fixture.Bicycles
            .Where(b => !rentedIds.Contains(b.Id))
            .ToList();

        Assert.NotEmpty(unrented); // у нас есть велосипеды, которые не брали
        Assert.Equal(2, unrented.Count);
        Assert.True(unrented.All(b => !rentedIds.Contains(b.Id)));
    }
}
