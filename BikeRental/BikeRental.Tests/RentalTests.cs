using System;
using System.Linq;
using Xunit;
using BikeRental.Domain;
using BikeRental.Domain.Enums;
using BikeRental.Tests;

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
            .Where(b => b.Model.Type == BikeType.Sports)
            .ToList();

        Assert.NotEmpty(sportBikes);
        Assert.True(sportBikes.All(b => b.Model.Type == BikeType.Sports));
        Assert.Equal(4, sportBikes.Count);
    }

    /// <summary>
    /// Checks that the top five bicycle models are selected
    /// correctly when sorted by total rental profit.
    /// </summary>
    [Fact]
    public void ShouldReturnTop5ModelsByProfit()
    {
        var top = fixture.Rentals
            .GroupBy(r => r.Bicycle.Model)
            .Select(g => new
            {
                Model = g.Key.Name,
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
            .GroupBy(r => r.Bicycle.Model)
            .Select(g => new
            {
                Model = g.Key.Name,
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

        Assert.Equal(2, durations.Min());
        Assert.Equal(7, durations.Max());
        Assert.Equal(4.5, durations.Average(), precision: 3);
    }

    /// <summary>
    /// Checks that total rental time is correctly calculated
    /// for each bicycle type (sports, urban, mountain, childrens).
    /// </summary>
    [Fact]
    public void ShouldReturnTotalRentalTimeByBikeType()
    {
        var byType = fixture.Rentals
            .GroupBy(r => r.Bicycle.Model.Type)
            .Select(g => new { Type = g.Key, TotalHours = g.Sum(r => r.DurationHours) })
            .ToDictionary(x => x.Type, x => x.TotalHours);

        Assert.Equal(20, byType[BikeType.Sports]);
        Assert.Equal(10, byType[BikeType.Urban]);
        Assert.Equal(9, byType[BikeType.Mountain]);
        Assert.Equal(6, byType[BikeType.Childrens]);
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
            .Select(g => new { Renter = g.Key.FullName, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        Assert.NotEmpty(stats);
        var max = stats.Max(x => x.Count);
        Assert.Equal(1, max);
    }
}
