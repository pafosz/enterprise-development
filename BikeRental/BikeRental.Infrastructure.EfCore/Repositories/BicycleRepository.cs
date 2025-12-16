using BikeRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Provides CRUD operations for <see cref="Bicycle"/> entities using <see cref="BikeRentalDbContext"/>.
/// </summary>
/// <param name="dbContext">The EF Core database context.</param>
public sealed class BicycleRepository(BikeRentalDbContext dbContext) : IRepository<Bicycle, int>
{
    /// <summary>
    /// Creates a new <see cref="Bicycle"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    public async Task<Bicycle> Create(Bicycle entity)
    {
        dbContext.Bicycles.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Reads a <see cref="Bicycle"/> entity by its identifier.
    /// Returned entity is tracked by the context.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<Bicycle?> Read(int entityId) =>
        dbContext.Bicycles
            .Include(b => b.Model)
            .FirstOrDefaultAsync(b => b.Id == entityId);

    /// <summary>
    /// Reads all <see cref="Bicycle"/> entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IList<Bicycle>> ReadAll() =>
        await dbContext.Bicycles
            .AsNoTracking()
            .Include(b => b.Model)
            .ToListAsync();

    /// <summary>
    /// Updates an existing <see cref="Bicycle"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    public async Task<Bicycle> Update(Bicycle entity)
    {
        dbContext.Bicycles.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a <see cref="Bicycle"/> entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.Bicycles.FindAsync(entityId);
        if (entity is null)
            return false;

        dbContext.Bicycles.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
