using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Operations.Revisions;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace OpenFibu.Data.RavenDb;

[Obsolete("Configure DI and use injection of IAsyncDocumentSession instead")]
public class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> _store = new(CreateDocumentStore);

    public static IDocumentStore Store => _store.Value;

    private static IDocumentStore CreateDocumentStore()
    {
        var database = "OpenFibu";
        IDocumentStore documentStore = new DocumentStore
        {
            Urls = new[] { "http://localhost:8080" },
            Database = database,
        };

        documentStore.Initialize();

        EnsureCreated(documentStore, database);
        return documentStore;
    }

    private static void EnsureCreated(IDocumentStore documentStore, string database)
    {
        try
        {
            documentStore.Maintenance.ForDatabase(database).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            try
            {
                documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
            }
            catch (ConcurrencyException)
            {
            }
        }
    }
}