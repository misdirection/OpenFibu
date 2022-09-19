using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities;

namespace OpenFibu.Application.Queries;

public record GetAllSteuerschluesselQuery() : IRequest<IEnumerable<SteuerschluesselDto>>;
public class GetAllSteuerschluesselQueryHandler : IRequestHandler<GetAllSteuerschluesselQuery, IEnumerable<SteuerschluesselDto>>
{
    private readonly IRepository<Steuerschluessel> _repository;

    public GetAllSteuerschluesselQueryHandler(IRepository<Steuerschluessel> repository) => _repository = repository;

    public async Task<IEnumerable<SteuerschluesselDto>> Handle(GetAllSteuerschluesselQuery request, CancellationToken cancellationToken)
    {
        var steuerschluesselListe = _repository.GetAll();

        var steuerschluesselDtos = new List<SteuerschluesselDto>();

        foreach (var steuerschluessel in steuerschluesselListe)
        {
            steuerschluesselDtos.Add(new SteuerschluesselDto(steuerschluessel.Id, steuerschluessel.Bezeichnung, steuerschluessel.Steuersatz, steuerschluessel.Steuerkonto));
        }
        return await Task.FromResult(steuerschluesselDtos);
    }
}
