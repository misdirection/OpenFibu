using OpenFibu.Domain.Journal.Entities;

namespace OpenFibu.Data.RavenDb;

public class GeschaeftsvorfallRepository : BaseRepository<Geschaeftsvorfall>
{
    public override Task UpdateAsync(Geschaeftsvorfall entity)
    {
        throw new NotImplementedException();
    }
}