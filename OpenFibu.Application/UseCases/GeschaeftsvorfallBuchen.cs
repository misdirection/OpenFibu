using MediatR;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Application.UseCases;

public sealed record GeschaeftsvorfallBuchenCommand(string GeschaeftsvorfallId) : IRequest;

public sealed class GeschaeftsvorfallBuchenCommandHandler : IRequestHandler<GeschaeftsvorfallBuchenCommand>
{
    private readonly IRepository<Geschaeftsvorfall> _repository;

    public GeschaeftsvorfallBuchenCommandHandler(IRepository<Geschaeftsvorfall> repository)
        => _repository = repository;


    public Task<Unit> Handle(GeschaeftsvorfallBuchenCommand request, CancellationToken cancellationToken)
    {
        var geschaeftsvorfall = _repository.Get(gv => gv.Id == request.GeschaeftsvorfallId);

        geschaeftsvorfall.Buchen();

        return Task.FromResult(Unit.Value);
    }
}