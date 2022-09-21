using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.UseCases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenFibu.Wpf.Vorkontierung;

public partial class VorkontierungViewModel : ObservableValidator
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VorkontierungViewModel(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    public VorkontierungViewModel()
    {
    }

    [ObservableProperty] 
    private string? _id;

    [ObservableProperty] 
    [Required] 
    private string _laufendeNummer = string.Empty;

    [ObservableProperty] 
    [Required] 
    private string _belegnummer = string.Empty;

    [ObservableProperty] 
    [Required] 
    private DateTime _belegdatum = DateTime.Now;

    [ObservableProperty] 
    [Required] 
    private DateTime _buchungsdatum = DateTime.Now;

    [ObservableProperty] 
    private uint? _sollkontonummer;
    
    public string? SollkontoId { get; set; }

    [ObservableProperty] 
    private uint? _habenkontonummer;
    
    public string? HabenkontoId { get; set; }

    [ObservableProperty] 
    private string? _steuerschluesselBezecihnung;

    public string? SteuerschluesselId { get; set; }

    [ObservableProperty] 
    private decimal? _betrag;

    [ObservableProperty] 
    private string _buchungstext = string.Empty;

    [RelayCommand]
    public void Speichern()
    {
        ValidateAllProperties();

        if (HasErrors)
            return;
        var kontierungszeilen = new List<KontierungszeilenDto>
        {
            new KontierungszeilenDto(null, SollkontoId, null, Betrag.Value),
            new KontierungszeilenDto(null, HabenkontoId, null, Betrag.Value)
        };
        if (Id is null)
        {
            Id = _mediator.Send(
                new VorkontierungErfassenCommand(
                    _mapper.Map<VorkontierungsDto>(this))).Result;
        }
        else
        {
            _mediator.Send(
                new VorkontierungBearbeitenCommand(_mapper.Map<VorkontierungsDto>(this)));
        }
    }
}