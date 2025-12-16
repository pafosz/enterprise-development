using BikeRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Provides CRUD operations for <see cref="Model"/> entities using <see cref="BikeRentalDbContext"/>.
/// </summary>
/// <param name="dbContext">The EF Core database context.</param>
public sealed class ModelRepository(BikeRentalDbContext dbContext) : IRepository<Model, int>
{
    /// <summary>
    /// Creates a new <see cref="Model"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    public async Task<Model> Create(Model entity)
    {
        dbContext.Models.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Reads a <see cref="Model"/> entity by its identifier.
    /// Returned entity is tracked by the context.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<Model?> Read(int entityId) =>
        dbContext.Models.FirstOrDefaultAsync(m => m.Id == entityId);

    /// <summary>
    /// Reads all <see cref="Model"/> entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IList<Model>> ReadAll() =>
        await dbContext.Models.AsNoTracking().ToListAsync();

    /// <summary>
    /// Updates an existing <see cref="Model"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    public async Task<Model> Update(Model entity)
    {
        dbContext.Models.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a <see cref="Model"/> entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.Models.FindAsync(entityId);
        if (entity is null)
            return false;

        dbContext.Models.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
