using Shared.Primitives;
using Shared.Results;
using System.Linq.Expressions;

namespace SearchBugs.Domain;

public interface IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : class, IEntityId
{
    Task<Result> Add(TEntity entity);

    Task<Result<TEntity>> GetByIdAsync(IEntityId id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    Task<Result> Remove(TEntity entity);

    Task<Result> Update(TEntity entity);
}