using OpenFibu.Domain.Stammdaten.Entities;
using Raven.Client.Documents.Session;

namespace OpenFibu.Data.RavenDb;

public class SteuerschluesselRepository : BaseRepository<Steuerschluessel>
{
    public SteuerschluesselRepository(IAsyncDocumentSession documentSession) : base(documentSession)
    {
    }

    public override Task UpdateAsync(Steuerschluessel entity)
    {
        throw new NotImplementedException();
    }
}