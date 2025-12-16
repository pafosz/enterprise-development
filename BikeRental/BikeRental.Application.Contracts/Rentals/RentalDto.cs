namespace BikeRental.Application.Contracts.Rentals;

/// <summary>
/// Data transfer object representing a rental record.
/// </summary>
/// <param name="Id">Unique identifier of the rental record.</param>
/// <param name="BicycleId">Unique identifier of the rented bicycle.</param>
/// <param name="RenterId">Unique identifier of the renter who made the rental.</param>
/// <param name="StartTime">Start time of the rental.</param>
/// <param name="DurationHours">Duration of the rental in hours.</param>
public sealed record RentalDto(
    int Id,
    int BicycleId,
    int RenterId,
    DateTime StartTime,
    int DurationHours);