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
using OpenFibu.Data.RavenDb.Configuration;
using Microsoft.Extensions.Configuration;
using OpenFibu.Wpf.Vorkontierung;

namespace OpenFibu.Wpf;

public partial class App : System.Windows.Application
{
    public new static App Current => (App)System.Windows.Application.Current;
    public IServiceProvider Services { get; }
    private readonly IConfiguration _configuration;
    private readonly bool _useMockDataStores;

    public App()
    {
        _configuration = BuildConfiguration();
        _useMockDataStores = _configuration.GetValue<bool>("UseMockDataStores");
        Services = ConfigureServices();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var window = Services.GetRequiredService<MainWindow>();

        window.Show();
        base.OnStartup(e);
    }

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        configurationBuilder.AddUserSecrets<App>();
        configurationBuilder.AddEnvironmentVariables();
        return configurationBuilder.Build();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        if (_useMockDataStores)
        {
            services.AddSingleton<IRepository<Geschaeftsvorfall>, BaseMockRepository<Geschaeftsvorfall>>();
            services.AddSingleton<IReadRepository<Geschaeftsvorfall>, BaseMockRepository<Geschaeftsvorfall>>();
            services.AddSingleton<IRepository<Steuerschluessel>, BaseMockRepository<Steuerschluessel>>();
            services.AddSingleton<IReadRepository<Steuerschluessel>, BaseMockRepository<Steuerschluessel>>();
            services.AddSingleton<IRepository<Domain.Vorkontierung.Entities.Vorkontierung>, BaseMockRepository<Domain.Vorkontierung.Entities.Vorkontierung>>();
            services.AddSingleton<IReadRepository<Domain.Vorkontierung.Entities.Vorkontierung>, BaseMockRepository<Domain.Vorkontierung.Entities.Vorkontierung>>();
        }
        else
        {
            services.AddTransient<IRepository<Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
            services.AddSingleton<IReadRepository<Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
            services.AddSingleton<IRepository<Steuerschluessel>, SteuerschluesselRepository>();
            services.AddSingleton<IReadRepository<Steuerschluessel>, SteuerschluesselRepository>();
            services.AddSingleton<IRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();
            services.AddSingleton<IReadRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();
        }


        services.InitiliazeDocumentStore(_configuration);
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