using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenFibu.Wpf.Buchung;

public partial class BuchungserfassungViewModel : ObservableObject
{
    [ObservableProperty]
    private uint? _soll;

    [ObservableProperty]
    private uint? _haben;

    [ObservableProperty]
    private decimal? _betrag;
}
