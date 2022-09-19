using OpenFibu.Wpf;
using OpenFibu.Wpf.Buchung;
using OpenFibu.Wpf.Common;
using OpenFibu.Wpf.Geschaeftsvorfall;
using OpenFibu.Wpf.Stammdaten;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OpenFibu.Application.Interfaces;
using OpenFibu.Application.Queries;
using OpenFibu.Data.Mock;
using OpenFibu.Data.RavenDb;
using OpenFibu.Domain.Entities;
using OpenFibu.Wpf.Geschaeftsvorfall;
using System;
using System.Windows;

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
        services.AddTransient<IRepository<OpenFibu.Domain.Entities.Journal.Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
        services.AddTransient<IReadRepository<OpenFibu.Domain.Entities.Journal.Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
        services.AddTransient<IRepository<Steuerschluessel>, SteuerschluesselMockRepo>();
        services.AddMediatR(typeof(GetAllSteuerschluesselQuery));
        services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<GeschaeftsvorfaelleViewModel>();
        services.AddTransient<GeschaeftsvorfallerfassungViewModel>();
        services.AddTransient<BuchungserfassungViewModel>();
        services.AddTransient<SteuerschluesselViewModel>();
        services.AddTransient<MainWindow>();
        return services.BuildServiceProvider();
    }
}
