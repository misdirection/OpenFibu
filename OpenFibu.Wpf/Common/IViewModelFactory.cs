using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenFibu.Wpf.Common;

public enum ViewModelType
{
    Geschaeftsvorfaelle,
    Steuerschluessel,
    Konten
}

public interface IViewModelFactory
{
    ObservableObject Create(ViewModelType type);
}
