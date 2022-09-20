using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Vorkontierung.Entities;

namespace OpenFibu.Application.Queries;


public record GetVorkontierungenQuery() : IRequest<IEnumerable<VorkontierungsDto>>;

public class GetVorkontierungenQueryHandler : IRequestHandler<GetVorkontierungenQuery,
    IEnumerable<VorkontierungsDto>>
{
    private readonly IReadRepository<Vorkontierung> _repository;

    public GetVorkontierungenQueryHandler(IReadRepository<Vorkontierung> repository) =>
        _repository = repository;

    public async Task<IEnumerable<VorkontierungsDto>> Handle(GetVorkontierungenQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        var vorkontierungsDtos = entities.Select(
            vorkontierung => new VorkontierungsDto()
            {
                Id = vorkontierung.Id,
                Belegnummer = vorkontierung.Belegnummer,
                Buchungsdatum = vorkontierung.Buchungsdatum,
                Belegdatum = vorkontierung.Belegdatum,
                Kontierungszeilen = vorkontierung.Kontierungszeilen.Select(
                    b => new KontierungszeilenDto()
                    {
                        Id = b.Id,
                        KontoId = b.KontoId,
                        Betrag = b.Betrag
                    })
            }).ToList();

        return vorkontierungsDtos;
    }
}