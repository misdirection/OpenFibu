using OpenFibu.Domain.Vorkontierung.Entities;

namespace OpenFibu.Data.RavenDb
{
    public class VorkontierungsRepository : BaseRepository<Vorkontierung>
    {
        public override async Task UpdateAsync(Vorkontierung entity)
        {
            using var session = DocumentStoreHolder.Store.OpenSession();
            var result = session.Load<Vorkontierung>(entity.Id);
            result.Buchungsdatum = entity.Buchungsdatum;
            result.Buchungstext = entity.Buchungstext;
            result.LaufendeNummer = entity.LaufendeNummer;
            result.Belegdatum = entity.Belegdatum;
            result.Belegnummer = entity.Belegnummer;
            session.SaveChanges();
            await Task.CompletedTask;
        }
    }
}