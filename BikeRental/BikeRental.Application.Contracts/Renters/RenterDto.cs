namespace BikeRental.Application.Contracts.Renters;

/// <summary>
/// Data transfer object representing a renter.
/// </summary>
/// <param name="Id">Unique identifier of the renter.</param>
/// <param name="FullName">Full name of the renter.</param>
/// <param name="Phone">Contact phone number of the renter.</param>
public sealed record RenterDto(
    int Id,
    string FullName,
    string? Phone);