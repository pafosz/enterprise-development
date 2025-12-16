using BikeRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Provides CRUD operations for <see cref="Rental"/> entities using <see cref="BikeRentalDbContext"/>.
/// </summary>
/// <param name="dbContext">The EF Core database context.</param>
public sealed class RentalRepository(BikeRentalDbContext dbContext) : IRepository<Rental, int>
{
    /// <summary>
    /// Creates a new <see cref="Rental"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    public async Task<Rental> Create(Rental entity)
    {
        dbContext.Rentals.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Reads a <see cref="Rental"/> entity by its identifier.
    /// Returned entity is tracked by the context.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<Rental?> Read(int entityId) =>
        dbContext.Rentals
            .Include(r => r.Renter)
            .Include(r => r.Bicycle)
                .ThenInclude(b => b!.Model)
            .FirstOrDefaultAsync(r => r.Id == entityId);

    /// <summary>
    /// Reads all <see cref="Rental"/> entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IList<Rental>> ReadAll() =>
        await dbContext.Rentals
            .AsNoTracking()
            .Include(r => r.Renter)
            .Include(r => r.Bicycle)
                .ThenInclude(b => b!.Model)
            .ToListAsync();

    /// <summary>
    /// Updates an existing <see cref="Rental"/> entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    public async Task<Rental> Update(Rental entity)
    {
        dbContext.Rentals.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Deletes a <see cref="Rental"/> entity by its identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to delete.</param>
    /// <returns>true if an entity was deleted; otherwise false.</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await dbContext.Rentals.FindAsync(entityId);
        if (entity is null)
            return false;

        dbContext.Rentals.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
