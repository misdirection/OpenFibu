using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Journal.Entities;
using OpenFibu.Domain.Shared.Enums;
using OpenFibu.Domain.Stammdaten.Entities;

namespace OpenFibu.Application.UseCases;

public record VorkontierungErfassenCommand(VorkontierungsDto Vorkontierung) : IRequest<string>;

internal class VorkontierungErfassenCommandHandler : IRequestHandler<VorkontierungErfassenCommand, string>
{
    private readonly IRepository<Vorkontierung> _repository;

    public VorkontierungErfassenCommandHandler(IRepository<Vorkontierung> repository) => _repository = repository;

    public async Task<string> Handle(VorkontierungErfassenCommand request, CancellationToken cancellationToken)
    {
        var vorkontierung = Vorkontierung.Erstellen( request.Vorkontierung.LaufendeNummer,request.Vorkontierung.Belegnummer, request.Vorkontierung.Buchungsdatum, request.Vorkontierung.Belegdatum, null);

        var steuerschluessel = new Steuerschluessel(); //Todo: muss noch geholt werden
        
        // foreach (var kontierungszeile in request.Vorkontierung.Kontierungszeilen)
        // {
        //     vorkontierung.KontierungszeileHinzufuegen(Kontierungszeile.Erstellen(kontierungszeile.Betrag,
        //         kontierungszeile.KontoId,
        //         kontierungszeile.SollHaben, steuerschluessel));
        // }

        _repository.Add(vorkontierung);

        return await Task.FromResult(vorkontierung.Id!);
    }
}