using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Stammdaten.Entities;
using OpenFibu.Domain.Vorkontierung.Entities;

namespace OpenFibu.Application.UseCases;

public record VorkontierungBearbeitenCommand(VorkontierungsDto Vorkontierung) : IRequest<string>;

internal class VorkontierungBearbeitenCommandHandler : IRequestHandler<VorkontierungBearbeitenCommand, string>
{
    private readonly IReadRepository<Vorkontierung> _readRepository;
    private readonly IRepository<Vorkontierung> _repository;

    public VorkontierungBearbeitenCommandHandler(IRepository<Vorkontierung> repository, IReadRepository<Vorkontierung> readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }

    public async Task<string> Handle(VorkontierungBearbeitenCommand request, CancellationToken cancellationToken)
    {
        var vorkontierung = await _readRepository.GetByIdAsync(request.Vorkontierung.Id);
        vorkontierung.Belegnummer = request.Vorkontierung.Belegnummer;
        vorkontierung.Belegdatum = request.Vorkontierung.Belegdatum;
        vorkontierung.LaufendeNummer = request.Vorkontierung.LaufendeNummer;

        var steuerschluessel = new Steuerschluessel(); //Todo: muss noch geholt werden

        // foreach (var kontierungszeile in request.Vorkontierung.Kontierungszeilen)
        // {
        //     vorkontierung.KontierungszeileHinzufuegen(Kontierungszeile.Erstellen(kontierungszeile.Betrag,
        //         kontierungszeile.KontoId,
        //         kontierungszeile.SollHaben, steuerschluessel));
        // }

        await _repository.UpdateAsync(vorkontierung);

        return await Task.FromResult(vorkontierung.Id!);
    }
}