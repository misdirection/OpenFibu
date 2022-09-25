using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace OpenFibu.Data.RavenDb.Configuration;

public static class DocumentStoreInitializer
{
    public static void InitiliazeDocumentStore(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("DocumentStoreSettings").Get<DocumentStoreSettings>();

        services.AddSingleton<IDocumentStore>(new DocumentStore()
        {
            Urls = settings.Urls,
            Database = settings.DatabaseName
        });

        services.AddScoped<IAsyncDocumentSession>(serviceProvider =>
        {
            var docStore = serviceProvider.GetService<IDocumentStore>();

            docStore!.Initialize();
            EnsureCreation(docStore, settings);

            return docStore.OpenAsyncSession();
        });
    }

    private static void EnsureCreation(IDocumentStore docStore, DocumentStoreSettings settings)
    {
        try
        {
            docStore.Maintenance.ForDatabase(settings.DatabaseName).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            try
            {
                docStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(settings.DatabaseName)));
            }
            catch (ConcurrencyException)
            {
            }
        }
    }
}