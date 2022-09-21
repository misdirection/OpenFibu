using System.Linq.Expressions;
using OpenFibu.Application.Interfaces;
using OpenFibu.Shared;

namespace OpenFibu.Data.Mock;

public class BaseMockRepository<T> : IRepository<T>, IReadRepository<T> where T : Entity,IAggregateRoot
{
    private List<T> _list = new();
    public async Task AddAsync(T entity)
    {
        _list.Add(entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(T entity)
    {
        var index = _list.FindIndex(t => t.Id == entity.Id);
        _list[index] = entity;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity)
    {
        _list.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> GetByIdAsync(string id) 
        => await Task.FromResult(_list.FirstOrDefault(t => t.Id == id));

    public async Task<IEnumerable<T>> GetAllAsync() => await Task.FromResult(_list);

    public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, 
        params Expression<Func<T, object>>[] includeProperties) 
        => await Task.FromResult(_list.Where(expression.Compile()));
}