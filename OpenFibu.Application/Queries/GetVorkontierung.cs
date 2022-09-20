using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Vorkontierung.Entities;

namespace OpenFibu.Application.Queries;

public record GetVorkontierungQuery(string Id) : IRequest<VorkontierungsDto?>;

public class GetVorkontierungQueryHandler : IRequestHandler<GetVorkontierungQuery, VorkontierungsDto?>
{
    private readonly IReadRepository<Vorkontierung> _repository;

    public GetVorkontierungQueryHandler(IReadRepository<Vorkontierung> repository) =>
        _repository = repository;

    public async Task<VorkontierungsDto?> Handle(GetVorkontierungQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);

        var vorkontierungsDto = new VorkontierungsDto(result.Id, result.LaufendeNummer, result.Belegnummer,
            result.Buchungsdatum, result.Belegdatum, Enumerable.Empty<KontierungszeilenDto>().ToList());
        return vorkontierungsDto;
    }
}