using OpenFibu.Domain.Vorkontierung.Entities;
using Raven.Client.Documents.Session;

namespace OpenFibu.Data.RavenDb
{
    public class VorkontierungsRepository : BaseRepository<Vorkontierung>
    {
        private readonly IAsyncDocumentSession _documentSession;

        public VorkontierungsRepository(IAsyncDocumentSession documentSession) : base(documentSession) 
            => _documentSession = documentSession;

        public override async Task UpdateAsync(Vorkontierung entity)
        {
            var result = await _documentSession.LoadAsync<Vorkontierung>(entity.Id);
            result.Buchungsdatum = entity.Buchungsdatum;
            result.Buchungstext = entity.Buchungstext;
            result.LaufendeNummer = entity.LaufendeNummer;
            result.Belegdatum = entity.Belegdatum;
            result.Belegnummer = entity.Belegnummer;
            await _documentSession.SaveChangesAsync();
        }
    }
}