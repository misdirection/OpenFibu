using OpenFibu.Application.Interfaces;
using OpenFibu.Shared;
using Raven.Client.Documents;
using System.Linq.Expressions;
using Raven.Client.Documents.Session;

namespace OpenFibu.Data.RavenDb;

public abstract class BaseRepository<T> : IReadRepository<T>, IRepository<T> where T : IAggregateRoot
{
    private readonly IAsyncDocumentSession _documentSession;

    protected BaseRepository(IAsyncDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public virtual async Task<T> GetByIdAsync(string id)
    {
        return await _documentSession.LoadAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var results = _documentSession.Query<T>().ToList();
        return await Task.FromResult(results);
    }

    public virtual async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = _documentSession.Query<T>();

        var list = includeProperties.Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty)).Where(expression).ToList();

        return await Task.FromResult(list);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _documentSession.StoreAsync(entity);
        await _documentSession.SaveChangesAsync();
    }

    public abstract Task UpdateAsync(T entity);

    public virtual async Task DeleteAsync(T entity)
    {
        _documentSession.Delete(entity);
        await _documentSession.SaveChangesAsync();
    }
}