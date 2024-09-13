using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain;
using Shared.Primitives;
using Shared.Results;
using System.Linq.Expressions;
namespace SearchBugs.Persistence.Repositories;

public abstract class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class, IEntityId
{
    protected readonly ApplicationDbContext _context;

    protected Repository(ApplicationDbContext dbContext) => _context = dbContext;

    public async Task<Result> Add(TEntity entity) => Result.Create(_context.Set<TEntity>().Add(entity));

    public async Task<Result> Update(TEntity entity) => Result.Create(_context.Set<TEntity>().Update(entity));

    public async Task<Result> Remove(TEntity entity) => Result.Create(_context.Set<TEntity>().Remove(entity));

    public async Task<Result<TEntity>> GetByIdAsync(IEntityId id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (includes.Length == 0)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return Result.Create(await query.FirstOrDefaultAsync(entity => entity.Id == id));
    }
}