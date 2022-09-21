using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.UseCases;
using OpenFibu.Domain.Vorkontierung.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenFibu.Wpf.Vorkontierung;

public partial class VorkontierungViewModel : ObservableValidator
{
    public VorkontierungViewModel() { }

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
    private string? _sollkontobezeichnung;
    public string? SollkontoId { get; set; }

    [ObservableProperty]
    private string? _habenkontobezeichnung;
    public string? HabenkontoId { get; set; }

    [ObservableProperty]
    private string? _steuerschluesselBezecihnung;

    public string? SteuerschluesselId { get; set; }

    [ObservableProperty]
    private decimal? _betrag;

    [ObservableProperty]
    private string _buchungstext = string.Empty;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VorkontierungViewModel(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [RelayCommand]
    public void Speichern()
    {
        ValidateAllProperties();

        if (HasErrors)
        {

        }
        else
        {
            var kontierungszeilen = new List<KontierungszeilenDto>
            {
            new KontierungszeilenDto(null, SollkontoId,null,Betrag.Value),
            new KontierungszeilenDto(null, HabenkontoId,null,Betrag.Value)
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
}