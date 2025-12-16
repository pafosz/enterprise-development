namespace BikeRental.Application.Contracts.Renters;

/// <summary>
/// Data transfer object used to create or update a renter.
/// </summary>
/// <param name="FullName">Full name of the renter.</param>
/// <param name="Phone">Contact phone number of the renter.</param>
public sealed record RenterCreateUpdateDto(
    string FullName,
    string? Phone);