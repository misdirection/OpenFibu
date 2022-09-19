using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Application.Queries
{
    public record HoleGeschaeftsvorfaelleQuery() : IRequest<IEnumerable<GeschaeftsvorfallDto>>;
    public class HoleGeschaeftsvorfaelleQueryHandler : IRequestHandler<HoleGeschaeftsvorfaelleQuery, IEnumerable<GeschaeftsvorfallDto>>
    {
        private readonly IReadRepository<Geschaeftsvorfall> _repository;

        public HoleGeschaeftsvorfaelleQueryHandler(IReadRepository<Geschaeftsvorfall> repository) => _repository = repository;

        public async Task<IEnumerable<GeschaeftsvorfallDto>> Handle(HoleGeschaeftsvorfaelleQuery request, CancellationToken cancellationToken)
        {
            var geschaeftsvorfaelle = new List<GeschaeftsvorfallDto>();

            var entities = await _repository.FindByConditionAsync(gv => gv.IstGebucht != false, gv => gv.Buchungen);
            var list = entities.Select(
                gv => new GeschaeftsvorfallDto()
                {
                    Id = gv.Id,
                    Belegnummer = gv.Belegnummer,
                    Buchungsdatum = gv.Buchungsdatum,
                    Belegdatum = gv.Belegdatum,
                    Buchungen = gv.Buchungen.Select(
                        b => new BuchungDto()
                        {
                            Id = b.Id,
                            Sollkonto = b.SollHaben == SollHaben.Soll ? b.Kontonummer : null,
                            Habenkonto = b.SollHaben == SollHaben.Haben ? b.Kontonummer : null,
                            Betrag = b.Betrag
                        })
                }).ToList();

            geschaeftsvorfaelle.AddRange(list);
            //foreach (var v in gv)
            //{
            //    var buchungen = new List<BuchungDto>();

            //    if (v.Buchungen is not null)
            //    {
            //        foreach (var buchung in v.Buchungen)
            //        {
            //            if (buchung.SollHaben == SollHaben.Soll)
            //                buchungen.Add(new BuchungDto(buchung.Id, buchung.Kontonummer, null, buchung.Betrag));
            //            else
            //                buchungen.Add(new BuchungDto(buchung.Id, null, buchung.Kontonummer, buchung.Betrag));
            //        }
            //    }
            //    geschaeftsvorfaelle.Add(new GeschaeftsvorfallDto(v.Id, v.Belegnummer, v.Buchungsdatum, v.Belegdatum, buchungen));
            //}

            return geschaeftsvorfaelle;
        }
    }
}
