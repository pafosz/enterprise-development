using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bicycles;
using BikeRental.Domain;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides application-level CRUD operations for bicycles.
/// </summary>
/// <param name="bicycleRepository">Repository used to access bicycle data.</param>
/// <param name="modelRepository">Repository used to validate referenced model identifiers.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class BicycleService(
    IRepository<Bicycle, int> bicycleRepository,
    IRepository<Model, int> modelRepository,
    IMapper mapper) : IApplicationService<BicycleDto, BicycleCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new bicycle based on the provided DTO.
    /// Validates that the referenced model exists.
    /// </summary>
    /// <param name="dto">The DTO containing data for creation.</param>
    /// <returns>The created DTO.</returns>
    public async Task<BicycleDto> Create(BicycleCreateUpdateDto dto)
    {
        _ = await modelRepository.Read(dto.ModelId) ?? throw new InvalidOperationException($"Model with id '{dto.ModelId}' was not found.");
        var entity = mapper.Map<Bicycle>(dto);
        var created = await bicycleRepository.Create(entity);
        return mapper.Map<BicycleDto>(created);
    }

    /// <summary>
    /// Gets a bicycle DTO by its identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO.</param>
    /// <returns>The DTO if found; otherwise null.</returns>
    public async Task<BicycleDto?> Get(int dtoId)
    {
        var entity = await bicycleRepository.Read(dtoId);
        return entity is null ? null : mapper.Map<BicycleDto>(entity);
    }

    /// <summary>
    /// Gets all bicycles.
    /// </summary>
    /// <returns>A list of all DTOs.</returns>
    public async Task<IList<BicycleDto>> GetAll()
    {
        var entities = await bicycleRepository.ReadAll();
        return [.. entities.Select(mapper.Map<BicycleDto>)];
    }

    /// <summary>
    /// Updates an existing bicycle identified by the provided identifier using the provided DTO.
    /// Validates that the referenced model exists.
    /// </summary>
    /// <param name="dto">The DTO containing updated data.</param>
    /// <param name="dtoId">The unique identifier of the DTO to update.</param>
    /// <returns>The updated DTO.</returns>
    public async Task<BicycleDto> Update(BicycleCreateUpdateDto dto, int dtoId)
    {
        var entity = await bicycleRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Bicycle with id '{dtoId}' was not found.");
        _ = await modelRepository.Read(dto.ModelId) ?? throw new InvalidOperationException($"Model with id '{dto.ModelId}' was not found.");
        mapper.Map(dto, entity);

        var updated = await bicycleRepository.Update(entity);
        return mapper.Map<BicycleDto>(updated);
    }

    /// <summary>
    /// Deletes a bicycle identified by the provided identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(int dtoId) => bicycleRepository.Delete(dtoId);
}
