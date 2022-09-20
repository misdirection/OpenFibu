using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace OpenFibu.Wpf.Vorkontierung
{
    public partial class VorkontierungViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _id = string.Empty;
        [ObservableProperty]
        private string _laufendeNummer = string.Empty;
        [ObservableProperty]
        private string _belegnummer = string.Empty;
        [ObservableProperty]
        private DateTime? _buchungsdatum = DateTime.Now;
        [ObservableProperty]
        private DateTime? _belegdatum = DateTime.Now;
        //public IEnumerable<KontierungszeilenDto> Kontierungszeilen { get; init; }
    }
}
