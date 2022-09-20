using MediatR;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Stammdaten.Entities;

namespace OpenFibu.Application.UseCases;

public record ErstelleSteuerschluesselCommand(string Bezeichnung, uint Steuerkonto, decimal Steuersatz) : IRequest;

internal class ErstelleSteuerschluesselCommandHandler : IRequestHandler<ErstelleSteuerschluesselCommand>
{
    private readonly IRepository<Steuerschluessel> _repository;

    public ErstelleSteuerschluesselCommandHandler(IRepository<Steuerschluessel> repository) => _repository = repository;

    public async Task<Unit> Handle(ErstelleSteuerschluesselCommand request, CancellationToken cancellationToken)
    {
        var neuerSteuerschluessel = Steuerschluessel.Erstellen(request.Bezeichnung, request.Steuerkonto, request.Steuersatz);
        await _repository.AddAsync(neuerSteuerschluessel);
        return await Task.FromResult(Unit.Value);
    }
}