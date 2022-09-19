using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using OpenFibu.Application.DTO;
using OpenFibu.Application.Queries;
using OpenFibu.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenFibu.Wpf.Geschaeftsvorfall
{
    public partial class GeschaeftsvorfaelleViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        private GeschaeftsvorfallerfassungViewModel _geschaeftsvorfallerfassungViewModel;
        
        [ObservableProperty]
        private ObservableCollection<GeschaeftsvorfallDto> _geschaeftsvorfaelle;
        public GeschaeftsvorfaelleViewModel(
            IMediator mediator, 
            GeschaeftsvorfallerfassungViewModel geschaeftsvorfallerfassungViewModel)
        {
            _mediator = mediator;
            _geschaeftsvorfallerfassungViewModel = geschaeftsvorfallerfassungViewModel;
            var gv = mediator.Send(new HoleGeschaeftsvorfaelleQuery()).Result;
            _geschaeftsvorfaelle = new(gv);
        }
        
        [RelayCommand]
        public void Speichern()
        {
            var buchungen = new List<BuchungDto>
            {
                new BuchungDto(
                    null,
                    GeschaeftsvorfallerfassungViewModel.BuchungserfassungViewModel.Soll,
                    null,
                    GeschaeftsvorfallerfassungViewModel.BuchungserfassungViewModel.Betrag.Value),
                new BuchungDto(
                    null,
                    null,
                    GeschaeftsvorfallerfassungViewModel.BuchungserfassungViewModel.Haben,
                    GeschaeftsvorfallerfassungViewModel.BuchungserfassungViewModel.Betrag.Value)
            };

            var id = _mediator.Send(
                new SpeichernCommand(
                    new GeschaeftsvorfallDto(
                        null,
                        GeschaeftsvorfallerfassungViewModel.Belegnummer,
                        DateOnly.FromDateTime(GeschaeftsvorfallerfassungViewModel.Buchungsdatum),
                        DateOnly.FromDateTime(GeschaeftsvorfallerfassungViewModel.Belegdatum),
                        buchungen))).Result;
            var holeGv = _mediator.Send(new HoleGeschaeftsvorfallQuery(id)).Result;
            Geschaeftsvorfaelle.Add(holeGv);
        }
    }
}
