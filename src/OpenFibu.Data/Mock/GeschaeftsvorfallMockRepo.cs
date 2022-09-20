using OpenFibu.Application.Interfaces;
using System.Linq.Expressions;
using OpenFibu.Domain.Journal.Entities;

namespace OpenFibu.Data.Mock;

public sealed class GeschaeftsvorfallMockRepo
{
    private List<Geschaeftsvorfall> _geschaeftsvorfaelle = new();
    public GeschaeftsvorfallMockRepo() => Seed();

    public IEnumerable<Geschaeftsvorfall> GetAll(Expression<Func<Geschaeftsvorfall, bool>>? predicate = null)
        => _geschaeftsvorfaelle.Where(predicate.Compile());

    public Geschaeftsvorfall Get(Expression<Func<Geschaeftsvorfall, bool>> predicate)
        => _geschaeftsvorfaelle.Where(predicate.Compile()).FirstOrDefault();

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
        var result = _geschaeftsvorfaelle.Where(expression.Compile());

        return result.Any()
            ? await Task.FromResult(result)
            : await Task.FromResult(Enumerable.Empty<Geschaeftsvorfall>());
    }
}