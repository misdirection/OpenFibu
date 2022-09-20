using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Queries;
using OpenFibu.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenFibu.Domain.Journal.Entities;

namespace OpenFibu.Wpf.Geschaeftsvorfall;

public partial class VorkontierungViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    
    [ObservableProperty]
    private ObservableCollection<GeschaeftsvorfallDto> _geschaeftsvorfaelle;
    
    [ObservableProperty]
    private string _belegnummer = string.Empty;

    [ObservableProperty]
    private DateTime _buchungsdatum = DateTime.Now;

    [ObservableProperty]
    private DateTime _belegdatum = DateTime.Now;

    [ObservableProperty]
    private string _buchungstext = string.Empty;
    
    [ObservableProperty]
    private uint? _soll;

    [ObservableProperty]
    private uint? _haben;

    [ObservableProperty]
    private decimal? _betrag;

    private string? _vorkontierungsId;
    
    public VorkontierungViewModel(
        IMediator mediator)
    {
        _mediator = mediator;
        var gv = mediator.Send(new HoleGeschaeftsvorfaelleQuery()).Result;
        _geschaeftsvorfaelle = new ObservableCollection<GeschaeftsvorfallDto>(gv);
    }
        
    [RelayCommand]
    public void Speichern()
    {
        var kontierungszeilen = new List<KontierungszeilenDto>
        {
            new KontierungszeilenDto(),
            new KontierungszeilenDto()
        };
        if (_vorkontierungsId is null)
        {
            _vorkontierungsId = _mediator.Send(
                new VorkontierungErfassenCommand(
                    new VorkontierungsDto(
                        null,
                        "1",
                        Belegnummer,
                        DateOnly.FromDateTime(Buchungsdatum),
                        DateOnly.FromDateTime(Belegdatum),
                        kontierungszeilen))).Result;
        }
        else
        {
            _mediator.Send(
                new VorkontierungBearbeitenCommand(
                    new VorkontierungsDto(
                        _vorkontierungsId,
                        "1",
                        Belegnummer,
                        DateOnly.FromDateTime(Buchungsdatum),
                        DateOnly.FromDateTime(Belegdatum),
                        kontierungszeilen)));
        }
        // var holeGv = _mediator.Send(new HoleGeschaeftsvorfallQuery(id)).Result;
        // Geschaeftsvorfaelle.Add(holeGv);
    }
}