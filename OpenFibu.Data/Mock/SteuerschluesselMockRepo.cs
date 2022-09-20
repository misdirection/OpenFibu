using OpenFibu.Application.Interfaces;
using System.Linq.Expressions;
using OpenFibu.Domain.Stammdaten.Entities;

namespace OpenFibu.Data.Mock;

public sealed class SteuerschluesselMockRepo 
{
    private List<Steuerschluessel> _steuerschluessel = new();
    public IEnumerable<Steuerschluessel> GetAll(Expression<Func<Steuerschluessel, bool>>? predicate = null)
    {
        return _steuerschluessel.Where(predicate.Compile());
    }

    public Steuerschluessel Get(Expression<Func<Steuerschluessel, bool>> predicate)
    {
        return _steuerschluessel.Where(predicate.Compile()).FirstOrDefault();
    }

    public void Add(Steuerschluessel entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        _steuerschluessel.Add(entity);
    }

    public void Update(Steuerschluessel entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Steuerschluessel entity) => _steuerschluessel.Remove(entity);
}