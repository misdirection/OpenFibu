using System.Linq.Expressions;
using OpenFibu.Domain.Journal.Entities;

namespace OpenFibu.Application.Interfaces;

public interface IReadRepository<T>
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
}