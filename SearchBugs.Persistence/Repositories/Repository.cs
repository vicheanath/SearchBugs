using Microsoft.EntityFrameworkCore;
using Shared.Primitives;

namespace SearchBugs.Persistence.Repositories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class, IEntityId
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}