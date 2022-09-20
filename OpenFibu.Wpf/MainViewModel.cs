using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenFibu.Wpf.Common;

namespace OpenFibu.Wpf;

public partial class MainWindowViewModel : ObservableObject
{

    private readonly IViewModelFactory _factory;

    [ObservableProperty]
    private ObservableObject _currentView;

    public MainWindowViewModel(IViewModelFactory factory)
    {
        _factory = factory;
        _currentView = _factory.Create(ViewModelType.Vorkontierungen);
    }

    [RelayCommand]
    public void ShowGeschaeftsvorfaelle() 
        => CurrentView = _factory.Create(ViewModelType.Vorkontierungen);

    [RelayCommand]
    public void ShowSteuerschluessel()
        => CurrentView = _factory.Create(ViewModelType.Steuerschluessel);

    [RelayCommand]
    public void ShowJournal()
    {
        //CurrentView = new GeschaeftsvorfallerfassungViewModel();
    }
}