using CommunityToolkit.Mvvm.ComponentModel;
using OpenFibu.Wpf.Stammdaten;
using Microsoft.Extensions.DependencyInjection;
using System;
using VorkontierungserfassungViewModel = OpenFibu.Wpf.Vorkontierung.VorkontierungserfassungViewModel;

namespace OpenFibu.Wpf.Common;

internal class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _provider;

    public ViewModelFactory(IServiceProvider provider) => _provider = provider;

    public ObservableObject Create(ViewModelType type)
    {
        switch (type)
        {
            case ViewModelType.Vorkontierungen:
                return _provider.GetRequiredService<VorkontierungserfassungViewModel>();
            case ViewModelType.Steuerschluessel:
                return _provider.GetRequiredService<SteuerschluesselViewModel>();
            default:
                throw new ArgumentException("ViewModel for ViewModelType: " + type + " not yet specified."); ;
        }
        throw new Exception();
    }
}