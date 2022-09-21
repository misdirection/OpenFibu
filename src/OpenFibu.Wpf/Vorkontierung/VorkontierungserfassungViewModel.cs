using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Queries;
using OpenFibu.Application.UseCases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenFibu.Wpf.Vorkontierung;

public partial class VorkontierungserfassungViewModel : ObservableObject
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    [ObservableProperty] 
    private VorkontierungViewModel _vorkontierung;
    
    [ObservableProperty] 
    private ObservableCollection<VorkontierungViewModel> _vorkontierungen = new();

    public VorkontierungserfassungViewModel(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        var vks = mediator.Send(new GetVorkontierungenQuery()).Result;
        foreach (var vk in vks)
        {
            var vkVm = _mapper.Map<VorkontierungViewModel>(vk);
            Vorkontierungen.Add(vkVm);
        }
        Vorkontierung = Vorkontierungen.First();
    }

    

    [RelayCommand]
    public void Neu()
    {
        Vorkontierung = new VorkontierungViewModel(_mediator,_mapper);
        Vorkontierungen.Add(Vorkontierung);
    }
}
