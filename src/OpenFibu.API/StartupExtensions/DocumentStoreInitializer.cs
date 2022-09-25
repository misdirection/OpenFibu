using OpenFibu.Data.RavenDb.Configuration;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace OpenFibu.API.StartupExtensions;

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

        services.AddScoped<IAsyncDocumentSession>(serviceProvider => serviceProvider.GetService<IDocumentStore>()!.OpenAsyncSession());
    }
}