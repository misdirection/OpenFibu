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
using System.Windows.Controls;

namespace OpenFibu.Wpf.Vorkontierung;

public partial class VorkontierungserfassungViewModel : ObservableObject
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    [ObservableProperty] private VorkontierungViewModel _vorkontierung;

    [ObservableProperty] private ObservableCollection<VorkontierungViewModel> _vorkontierungen = new();

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
    public void Speichern()
    {
        var kontierungszeilen = new List<KontierungszeilenDto>
        {
            new KontierungszeilenDto(),
            new KontierungszeilenDto()
        };
        if (_vorkontierung.Id is null)
        {
            _vorkontierung.Id = _mediator.Send(
                new VorkontierungErfassenCommand(
                    _mapper.Map<VorkontierungsDto>(_vorkontierung))).Result;
            var result = _mediator.Send(new GetVorkontierungQuery(_vorkontierung.Id)).Result;
            if (result is not null)
                _vorkontierungen.Add(_vorkontierung);
        }
        else
        {
            _mediator.Send(
                new VorkontierungBearbeitenCommand(_mapper.Map<VorkontierungsDto>(_vorkontierung)));
        }
    }
}