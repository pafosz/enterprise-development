using BikeRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Provides CRUD operations for <see cref="Renter"/> entities using <see cref="BikeRentalDbContext"/>.
/// </summary>
/// <param name="dbContext">The EF Core database context.</param>
public sealed class RenterRepository(BikeRentalDbContext dbContext) : IRepository<Renter, int>
{
    /// <summary>
    /// Creates a new <see cref="Renter"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    public async Task<Renter> Create(Renter entity)
    {
        dbContext.Renters.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Reads a <see cref="Renter"/> entity by its identifier.
    /// Returned entity is tracked by the context.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<Renter?> Read(int entityId) =>
        dbContext.Renters.FirstOrDefaultAsync(r => r.Id == entityId);

    /// <summary>
    /// Reads all <see cref="Renter"/> entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IList<Renter>> ReadAll() =>
        await dbContext.Renters.AsNoTracking().ToListAsync();

    /// <summary>
    /// Updates an existing <see cref="Renter"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    public async Task<Renter> Update(Renter entity)
    {
        dbContext.Renters.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a <see cref="Renter"/> entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.Renters.FindAsync(entityId);
        if (entity is null)
            return false;

        dbContext.Renters.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
