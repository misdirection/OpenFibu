namespace OpenFibu.Application.Interfaces;

public interface IRepository<in T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}