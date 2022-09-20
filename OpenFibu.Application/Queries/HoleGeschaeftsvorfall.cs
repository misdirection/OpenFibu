using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Journal.Entities;
using OpenFibu.Domain.Shared.Enums;

namespace OpenFibu.Application.Queries;

public record HoleGeschaeftsvorfallQuery(string Id) : IRequest<GeschaeftsvorfallDto>;

public class HoleGeschaeftsvorfallQueryHandler : IRequestHandler<HoleGeschaeftsvorfallQuery, GeschaeftsvorfallDto>
{
    private readonly IReadRepository<Geschaeftsvorfall> _repository;

    public HoleGeschaeftsvorfallQueryHandler(IReadRepository<Geschaeftsvorfall> repository) => _repository = repository;

    public async Task<GeschaeftsvorfallDto> Handle(HoleGeschaeftsvorfallQuery request,
        CancellationToken cancellationToken)
    {
        var gv = await _repository.GetByIdAsync(request.Id);
        var buchungen = gv.Buchungen.Select(buchung => buchung.SollHaben == SollHaben.Soll
                ? new BuchungDto(buchung.Id, buchung.Kontonummer, null, buchung.Betrag)
                : new BuchungDto(buchung.Id, null, buchung.Kontonummer, buchung.Betrag))
            .ToList();

        var geschaeftsvorfall =
            new GeschaeftsvorfallDto(gv.Id, gv.Belegnummer, gv.Buchungsdatum, gv.Belegdatum, buchungen);

        return geschaeftsvorfall;
    }
}