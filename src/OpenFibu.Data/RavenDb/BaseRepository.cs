using OpenFibu.Application.Interfaces;
using OpenFibu.Shared;
using Raven.Client.Documents;
using System.Linq.Expressions;

namespace OpenFibu.Data.RavenDb;

public abstract class BaseRepository<T> : IReadRepository<T>, IRepository<T> where T : IAggregateRoot
{
    public virtual async Task<T> GetByIdAsync(string id)
    {
        using var session = DocumentStoreHolder.Store.OpenSession();
        return await Task.FromResult(session.Load<T>(id));
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using var session = DocumentStoreHolder.Store.OpenSession();
        var results = session.Query<T>().ToList();
        return await Task.FromResult(results);
    }

    public virtual async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
    {
        using var session = DocumentStoreHolder.Store.OpenSession();
        var query = session.Query<T>();

        var list = Queryable.Where(includeProperties.Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty)), expression).ToList();

        return await Task.FromResult(list);
    }

    public virtual async Task AddAsync(T entity)
    {
        using var session = DocumentStoreHolder.Store.OpenSession();
        session.Store(entity);
        session.SaveChanges();
        await Task.CompletedTask;
    }

    public abstract Task UpdateAsync(T entity);
    public virtual async Task DeleteAsync(T entity)
    {
        using var session = DocumentStoreHolder.Store.OpenSession();
        session.Delete(entity);
        session.SaveChanges();
        await Task.CompletedTask;
    }
}