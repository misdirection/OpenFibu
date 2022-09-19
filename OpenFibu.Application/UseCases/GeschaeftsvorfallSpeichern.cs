using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Application.UseCases;

public record SpeichernCommand(GeschaeftsvorfallDto Geschaeftsvorfall) : IRequest<string>;

internal class SpeichernCommandHandler : IRequestHandler<SpeichernCommand, string>
{
    private readonly IRepository<Geschaeftsvorfall> _repository;

    public SpeichernCommandHandler(IRepository<Geschaeftsvorfall> repository) => _repository = repository;

    public async Task<string> Handle(SpeichernCommand request, CancellationToken cancellationToken)
    {
        var geschaeftsvorfall = Geschaeftsvorfall.Erstellen(request.Geschaeftsvorfall.Belegnummer, request.Geschaeftsvorfall.Buchungsdatum, request.Geschaeftsvorfall.Belegdatum, null);

        foreach (var buchung in request.Geschaeftsvorfall.Buchungen)
        {
            if (buchung.Sollkonto is not null)
            {
                geschaeftsvorfall.BuchungHinzufuegen(Buchung.Erstellen(buchung.Sollkonto.Value, buchung.Betrag, SollHaben.Soll));

            }
            else if (buchung.Habenkonto is not null)
            {
                geschaeftsvorfall.BuchungHinzufuegen(Buchung.Erstellen(buchung.Habenkonto.Value, buchung.Betrag, SollHaben.Haben));
            }
        }

        _repository.Add(geschaeftsvorfall);

        return await Task.FromResult(geschaeftsvorfall.Id!);
    }
}