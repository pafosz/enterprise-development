using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Rentals;
using BikeRental.Domain;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides application-level CRUD operations for rentals.
/// </summary>
/// <param name="rentalRepository">Repository used to access rental data.</param>
/// <param name="bicycleRepository">Repository used to validate referenced bicycle identifiers.</param>
/// <param name="renterRepository">Repository used to validate referenced renter identifiers.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class RentalService(
    IRepository<Rental, int> rentalRepository,
    IRepository<Bicycle, int> bicycleRepository,
    IRepository<Renter, int> renterRepository,
    IMapper mapper) : IApplicationService<RentalDto, RentalCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new rental based on the provided DTO.
    /// Validates that the referenced bicycle and renter exist.
    /// </summary>
    /// <param name="dto">The DTO containing data for creation.</param>
    /// <returns>The created DTO.</returns>
    public async Task<RentalDto> Create(RentalCreateUpdateDto dto)
    {
        _ = await bicycleRepository.Read(dto.BicycleId) ?? throw new InvalidOperationException($"Bicycle with id '{dto.BicycleId}' was not found.");
        _ = await renterRepository.Read(dto.RenterId) ?? throw new InvalidOperationException($"Renter with id '{dto.RenterId}' was not found.");
        var entity = mapper.Map<Rental>(dto);
        var created = await rentalRepository.Create(entity);
        return mapper.Map<RentalDto>(created);
    }

    /// <summary>
    /// Gets a rental DTO by its identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO.</param>
    /// <returns>The DTO if found; otherwise null.</returns>
    public async Task<RentalDto?> Get(int dtoId)
    {
        var entity = await rentalRepository.Read(dtoId);
        return entity is null ? null : mapper.Map<RentalDto>(entity);
    }

    /// <summary>
    /// Gets all rentals.
    /// </summary>
    /// <returns>A list of all DTOs.</returns>
    public async Task<IList<RentalDto>> GetAll()
    {
        var entities = await rentalRepository.ReadAll();
        return [.. entities.Select(mapper.Map<RentalDto>)];
    }

    /// <summary>
    /// Updates an existing rental identified by the provided identifier using the provided DTO.
    /// Validates that the referenced bicycle and renter exist.
    /// </summary>
    /// <param name="dto">The DTO containing updated data.</param>
    /// <param name="dtoId">The unique identifier of the DTO to update.</param>
    /// <returns>The updated DTO.</returns>
    public async Task<RentalDto> Update(RentalCreateUpdateDto dto, int dtoId)
    {
        var entity = await rentalRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Rental with id '{dtoId}' was not found.");
        _ = await bicycleRepository.Read(dto.BicycleId) ?? throw new KeyNotFoundException($"Bicycle with id '{dto.BicycleId}' was not found.");
        _ = await renterRepository.Read(dto.RenterId) ?? throw new KeyNotFoundException($"Renter with id '{dto.RenterId}' was not found.");
        mapper.Map(dto, entity);

        var updated = await rentalRepository.Update(entity);
        return mapper.Map<RentalDto>(updated);
    }

    /// <summary>
    /// Deletes a rental identified by the provided identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(int dtoId) => rentalRepository.Delete(dtoId);
}
