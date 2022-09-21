using OpenFibu.Shared;

namespace OpenFibu.Application.Interfaces;

public interface IRepository<in T> where T : IAggregateRoot
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}