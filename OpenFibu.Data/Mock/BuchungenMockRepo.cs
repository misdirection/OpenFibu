using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;
using System.Linq.Expressions;

namespace OpenFibu.Data.Mock;

public sealed class BuchungenMockRepo : IRepository<Geschaeftsvorfall>, IReadRepository<Geschaeftsvorfall>
{
    public BuchungenMockRepo() => Seed();

    private List<Geschaeftsvorfall> _geschaeftsvorfaelle = new();

    public IEnumerable<Geschaeftsvorfall> GetAll(Expression<Func<Geschaeftsvorfall, bool>>? predicate = null)
    {
        return _geschaeftsvorfaelle.Where(predicate.Compile());
    }

    public Geschaeftsvorfall Get(Expression<Func<Geschaeftsvorfall, bool>> predicate)
    {
        return _geschaeftsvorfaelle.Where(predicate.Compile()).FirstOrDefault();
    }

    public void Add(Geschaeftsvorfall entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        _geschaeftsvorfaelle.Add(entity);
    }

    public void Update(Geschaeftsvorfall entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Geschaeftsvorfall entity) => _geschaeftsvorfaelle.Remove(entity);

    private void Seed()
    {
    }

    public async Task<IEnumerable<Geschaeftsvorfall>> FindByConditionAsync(
        Expression<Func<Geschaeftsvorfall, bool>> expression,
        params Expression<Func<Geschaeftsvorfall, object>>[] includeProperties)
    {
        return await Task.FromResult(_geschaeftsvorfaelle.Where(expression.Compile()));
    }
}