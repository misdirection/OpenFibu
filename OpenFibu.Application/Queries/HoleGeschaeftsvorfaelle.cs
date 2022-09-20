using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Application.Queries
{
    public record HoleGeschaeftsvorfaelleQuery() : IRequest<IEnumerable<GeschaeftsvorfallDto>>;

    public class
        HoleGeschaeftsvorfaelleQueryHandler : IRequestHandler<HoleGeschaeftsvorfaelleQuery,
            IEnumerable<GeschaeftsvorfallDto>>
    {
        private readonly IReadRepository<Geschaeftsvorfall> _repository;

        public HoleGeschaeftsvorfaelleQueryHandler(IReadRepository<Geschaeftsvorfall> repository) =>
            _repository = repository;

        public async Task<IEnumerable<GeschaeftsvorfallDto>> Handle(HoleGeschaeftsvorfaelleQuery request,
            CancellationToken cancellationToken)
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

            return geschaeftsvorfaelle;
        }
    }
}