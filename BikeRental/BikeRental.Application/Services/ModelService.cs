using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Domain;
using BikeRental.Application.Contracts.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Provides application-level CRUD operations for bicycle models.
/// </summary>
/// <param name="repository">Repository used to access model data.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class ModelService(
    IRepository<Model, int> repository,
    IMapper mapper) : IApplicationService<ModelDto, ModelCreateUpdateDto, int>
{
    /// <summary>
    /// Creates a new model based on the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing data for creation.</param>
    /// <returns>The created DTO.</returns>
    public async Task<ModelDto> Create(ModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<Model>(dto);
        var created = await repository.Create(entity);
        return mapper.Map<ModelDto>(created);
    }

    /// <summary>
    /// Gets a model DTO by its identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO.</param>
    /// <returns>The DTO if found; otherwise null.</returns>
    public async Task<ModelDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId);
        return entity is null ? null : mapper.Map<ModelDto>(entity);
    }

    /// <summary>
    /// Gets all models.
    /// </summary>
    /// <returns>A list of all DTOs.</returns>
    public async Task<IList<ModelDto>> GetAll()
    {
        var entities = await repository.ReadAll();
        return [.. entities.Select(mapper.Map<ModelDto>)];
    }

    /// <summary>
    /// Updates an existing model identified by the provided identifier using the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated data.</param>
    /// <param name="dtoId">The unique identifier of the DTO to update.</param>
    /// <returns>The updated DTO.</returns>
    public async Task<ModelDto> Update(ModelCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId) ?? throw new KeyNotFoundException($"Model with id '{dtoId}' was not found.");
        mapper.Map(dto, entity);

        var updated = await repository.Update(entity);
        return mapper.Map<ModelDto>(updated);
    }

    /// <summary>
    /// Deletes a model identified by the provided identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(int dtoId) => repository.Delete(dtoId);
}
