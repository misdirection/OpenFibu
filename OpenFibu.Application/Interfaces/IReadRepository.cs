using System.Linq.Expressions;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Application.Interfaces
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<Geschaeftsvorfall>> FindByConditionAsync(Expression<Func<Geschaeftsvorfall, bool>> expression, params Expression<Func<Geschaeftsvorfall, object>>[] includeProperties);
    }
}
