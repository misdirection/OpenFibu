using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Queries;
using OpenFibu.Application.UseCases;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OpenFibu.Wpf.Stammdaten;

public partial class SteuerschluesselViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    [ObservableProperty] private string? _bezeichnung;

    [ObservableProperty] private uint? _steuerkonto;

    [ObservableProperty] private decimal? _steuersatz;

    [ObservableProperty] private ObservableCollection<SteuerschluesselDto> _steuerschluessel;

    public SteuerschluesselViewModel(IMediator mediator)
    {
        _mediator = mediator;
        _ = Load();
    }

    public async Task Load()
    {
        var result = await _mediator.Send(new GetAllSteuerschluesselQuery());
        _steuerschluessel = result is null ?new() : new(result);
    }

    [RelayCommand]
    private void Speichern()
    {
        if (_steuerkonto == null || _bezeichnung == null || _steuersatz == null) return;
        _mediator.Send(
            new ErstelleSteuerschluesselCommand(_bezeichnung, _steuerkonto.Value, _steuersatz.Value));
    }
}