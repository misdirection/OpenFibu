using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenFibu.Wpf.Common;

public enum ViewModelType
{
    Vorkontierungen,
    Steuerschluessel,
    Konten
}

public interface IViewModelFactory
{
    ObservableObject Create(ViewModelType type);
}