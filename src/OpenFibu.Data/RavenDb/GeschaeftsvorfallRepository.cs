using OpenFibu.Domain.Journal.Entities;
using Raven.Client.Documents.Session;

namespace OpenFibu.Data.RavenDb;

public class GeschaeftsvorfallRepository : BaseRepository<Geschaeftsvorfall>
{
    public GeschaeftsvorfallRepository(IAsyncDocumentSession documentSession) : base(documentSession)
    {
    }

    public override Task UpdateAsync(Geschaeftsvorfall entity)
    {
        throw new NotImplementedException();
    }
}