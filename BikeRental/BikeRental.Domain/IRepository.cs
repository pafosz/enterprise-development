namespace BikeRental.Domain;

/// <summary>
/// Defines a generic contract for CRUD operations over an entity type.
/// </summary>
/// <typeparam name="TEntity">The entity type managed by the repository.</typeparam>
/// <typeparam name="TKey">The value type of the entity identifier.</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity instance in the data store.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Reads an entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Reads all entities of the specified type.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Updates an existing entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public Task<bool> Delete(TKey entityId);
}
