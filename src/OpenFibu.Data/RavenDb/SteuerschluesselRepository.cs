using OpenFibu.Domain.Stammdaten.Entities;

namespace OpenFibu.Data.RavenDb;

public class SteuerschluesselRepository : BaseRepository<Steuerschluessel>
{
    public override Task UpdateAsync(Steuerschluessel entity)
    {
        throw new NotImplementedException();
    }
}