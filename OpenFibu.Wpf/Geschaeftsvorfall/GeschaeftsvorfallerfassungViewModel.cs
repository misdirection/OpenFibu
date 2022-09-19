using CommunityToolkit.Mvvm.ComponentModel;
using OpenFibu.Wpf.Buchung;
using System;

namespace OpenFibu.Wpf.Geschaeftsvorfall
{
    public partial class GeschaeftsvorfallerfassungViewModel : ObservableObject
    {
        [ObservableProperty]
        private BuchungserfassungViewModel _buchungserfassungViewModel;

        [ObservableProperty]
        private string _belegnummer = string.Empty;

        [ObservableProperty]
        private DateTime _buchungsdatum = DateTime.Now;

        [ObservableProperty]
        private DateTime _belegdatum = DateTime.Now;

        [ObservableProperty]
        private string _buchungstext = string.Empty;

        public GeschaeftsvorfallerfassungViewModel(BuchungserfassungViewModel buchungserfassungViewModel) 
            => _buchungserfassungViewModel = buchungserfassungViewModel;
    }
}
