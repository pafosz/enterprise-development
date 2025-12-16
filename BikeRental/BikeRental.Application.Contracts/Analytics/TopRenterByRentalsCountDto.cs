using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// Data transfer object representing renters ordered by the number of rentals they have made.
/// </summary>
/// <param name="Renter">Renter information.</param>
/// <param name="RentalsCount">Number of rentals made by the renter.</param>
public sealed record TopRenterByRentalsCountDto(
    RenterDto Renter,
    int RentalsCount);