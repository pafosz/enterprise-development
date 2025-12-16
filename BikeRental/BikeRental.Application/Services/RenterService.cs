using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Domain;
using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides application-level CRUD operations for renters.
/// </summary>
/// <param name="repository">Repository used to access renter data.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class RenterService(
    IRepository<Renter, int> repository,
    IMapper mapper) : IApplicationService<RenterDto, RenterCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new renter based on the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing data for creation.</param>
    /// <returns>The created DTO.</returns>
    public async Task<RenterDto> Create(RenterCreateUpdateDto dto)
    {
        var entity = mapper.Map<Renter>(dto);
        var created = await repository.Create(entity);
        return mapper.Map<RenterDto>(created);
    }

    /// <summary>
    /// Gets a renter DTO by its identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO.</param>
    /// <returns>The DTO if found; otherwise null.</returns>
    public async Task<RenterDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId);
        return entity is null ? null : mapper.Map<RenterDto>(entity);
    }

    /// <summary>
    /// Gets all renters.
    /// </summary>
    /// <returns>A list of all DTOs.</returns>
    public async Task<IList<RenterDto>> GetAll()
    {
        var entities = await repository.ReadAll();
        return [.. entities.Select(mapper.Map<RenterDto>)];
    }

    /// <summary>
    /// Updates an existing renter identified by the provided identifier using the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated data.</param>
    /// <param name="dtoId">The unique identifier of the DTO to update.</param>
    /// <returns>The updated DTO.</returns>
    public async Task<RenterDto> Update(RenterCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId) ?? throw new KeyNotFoundException($"Renter with id '{dtoId}' was not found.");
        mapper.Map(dto, entity);

        var updated = await repository.Update(entity);
        return mapper.Map<RenterDto>(updated);
    }

    /// <summary>
    /// Deletes a renter identified by the provided identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(int dtoId) => repository.Delete(dtoId);
}
