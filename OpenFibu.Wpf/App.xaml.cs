using OpenFibu.Wpf.Common;
using OpenFibu.Wpf.Stammdaten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OpenFibu.Application.Interfaces;
using OpenFibu.Application.Queries;
using OpenFibu.Data.Mock;
using OpenFibu.Data.RavenDb;
using System;
using System.Windows;
using OpenFibu.Domain.Journal.Entities;
using OpenFibu.Domain.Stammdaten.Entities;
using VorkontierungserfassungViewModel = OpenFibu.Wpf.Vorkontierung.VorkontierungserfassungViewModel;

namespace OpenFibu.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public App()
    {
        Services = ConfigureServices();
    }

    public new static App Current => (App)System.Windows.Application.Current;

    public IServiceProvider Services { get; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var window = Services.GetRequiredService<MainWindow>();

        window.Show();
        base.OnStartup(e);
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRepository<Geschaeftsvorfall>, BaseRepository<Geschaeftsvorfall>>();
        services.AddSingleton<IReadRepository<Geschaeftsvorfall>, BaseRepository<Geschaeftsvorfall>>();
        services.AddTransient<IRepository<Steuerschluessel>, BaseRepository<Steuerschluessel>>();
        services.AddTransient<IRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();
        services.AddTransient<IReadRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();

        services.AddMediatR(typeof(GetAllSteuerschluesselQuery));
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<VorkontierungserfassungViewModel>();
        services.AddTransient<SteuerschluesselViewModel>();
        services.AddTransient<MainWindow>();
        
        return services.BuildServiceProvider();
    }
}