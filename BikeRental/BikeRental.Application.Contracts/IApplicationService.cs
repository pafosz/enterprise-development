namespace BikeRental.Application.Contracts;

/// <summary>
/// Defines an application-level contract for CRUD operations over DTO types.
/// </summary>
/// <typeparam name="TDto">The DTO type returned by read operations.</typeparam>
/// <typeparam name="TCreateUpdateDto">The DTO type used for create and update operations.</typeparam>
/// <typeparam name="TKey">The value type of the DTO identifier.</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity based on the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing data for creation.</param>
    /// <returns>The created DTO.</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Gets a DTO by its identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO.</param>
    /// <returns>The DTO if found; otherwise null.</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Gets all DTOs of the specified type.
    /// </summary>
    /// <returns>A list of all DTOs.</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Updates an existing entity identified by the provided identifier using the provided DTO.
    /// </summary>
    /// <param name="dto">The DTO containing updated data.</param>
    /// <param name="dtoId">The unique identifier of the DTO to update.</param>
    /// <returns>The updated DTO.</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Deletes an entity identified by the provided identifier.
    /// </summary>
    /// <param name="dtoId">The unique identifier of the DTO to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(TKey dtoId);
}
