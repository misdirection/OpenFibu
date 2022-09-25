using System.Diagnostics.CodeAnalysis;
using MediatR;
using OpenFibu.API.StartupExtensions;
using OpenFibu.Application.Interfaces;
using OpenFibu.Application.Queries;
using OpenFibu.Data.Mock;
using OpenFibu.Data.RavenDb;
using OpenFibu.Domain.Journal.Entities;
using OpenFibu.Domain.Stammdaten.Entities;

namespace OpenFibu.API;

[ExcludeFromCodeCoverage(Justification = "We dont test startup classes")]
public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly bool _useMockDataStores;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _useMockDataStores = _configuration.GetValue<bool>("UseMockDataStores");
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //Mocks
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
            services.InitiliazeDocumentStore(_configuration);

            services.AddTransient<IRepository<Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
            services.AddSingleton<IReadRepository<Geschaeftsvorfall>, GeschaeftsvorfallRepository>();
            services.AddSingleton<IRepository<Steuerschluessel>, SteuerschluesselRepository>();
            services.AddSingleton<IReadRepository<Steuerschluessel>, SteuerschluesselRepository>();
            services.AddSingleton<IRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();
            services.AddSingleton<IReadRepository<Domain.Vorkontierung.Entities.Vorkontierung>, VorkontierungsRepository>();
        }


        services.AddMediatR(typeof(GetAllSteuerschluesselQuery));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}