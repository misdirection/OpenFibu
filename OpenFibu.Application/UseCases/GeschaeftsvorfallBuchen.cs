using MediatR;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Journal.Entities;

namespace OpenFibu.Application.UseCases;

public sealed record GeschaeftsvorfallBuchenCommand(string GeschaeftsvorfallId) : IRequest;

public sealed class GeschaeftsvorfallBuchenCommandHandler : IRequestHandler<GeschaeftsvorfallBuchenCommand>
{
    private readonly IRepository<Geschaeftsvorfall> _repository;
    private readonly IReadRepository<Geschaeftsvorfall> _readRepository;

    public GeschaeftsvorfallBuchenCommandHandler(IRepository<Geschaeftsvorfall> repository, IReadRepository<Geschaeftsvorfall> readRepository)
    {
        _repository = repository;
        _readRepository = readRepository;
    }


    public async Task<Unit> Handle(GeschaeftsvorfallBuchenCommand request, CancellationToken cancellationToken)
    {
        var geschaeftsvorfall = await _readRepository.GetByIdAsync(request.GeschaeftsvorfallId);

        geschaeftsvorfall.Buchen();

        return await Task.FromResult(Unit.Value);
    }
}