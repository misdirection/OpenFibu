using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.Queries;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
        _ = LoadAsync();
    }

    public async Task LoadAsync()
    {
        var vks = _mediator.Send(new GetVorkontierungenQuery()).Result;

        foreach (var vk in vks)
        {
            var vkVm = _mapper.Map<VorkontierungViewModel>(vk);
            Vorkontierungen.Add(vkVm);
        }

        if (Vorkontierungen.Any())
            Vorkontierung = Vorkontierungen.FirstOrDefault()!;
        else
        {
            Vorkontierung = new VorkontierungViewModel(_mediator, _mapper);
            Vorkontierungen.Add(Vorkontierung);
        }
    }

    [RelayCommand]
    public void Neu()
    {
        Vorkontierung = new VorkontierungViewModel(_mediator, _mapper);
        Vorkontierungen.Add(Vorkontierung);
    }
}
