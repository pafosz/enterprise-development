using BikeRental.Application.Contracts.Rentals;
using Bogus;

namespace BikeRental.Generator.RabbitMq.Host.Generator;

/// <summary>
/// Generates test rental contracts (<see cref="RentalCreateUpdateDto"/>) for RabbitMQ publishing.
/// </summary>
public static class RentalGenerator
{
    /// <summary>
    /// Generates a list of randomized rental contracts.
    /// </summary>
    /// <param name="count">Number of contracts to generate.</param>
    /// <returns>List of generated <see cref="RentalCreateUpdateDto"/> items.</returns>
    public static List<RentalCreateUpdateDto> GenerateRentals(int count)
    {
        var faker = new Faker<RentalCreateUpdateDto>()
            .CustomInstantiator(f =>
            {
                var bicycleId = f.Random.Int(min: 1, max: 24);
                var renterId = f.Random.Int(min: 1, max: 20);

                var startTime = f.Date.Recent(days: 30);

                var durationHours = f.Random.Int(min: 1, max: 72);

                return new RentalCreateUpdateDto(
                    BicycleId: bicycleId,
                    RenterId: renterId,
                    StartTime: startTime,
                    DurationHours: durationHours);
            });

        return faker.Generate(count);
    }
}