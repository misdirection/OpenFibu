using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Revisions;

namespace OpenFibu.Data.RavenDb;

public class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> _store = new(CreateDocumentStore);

    private static IDocumentStore CreateDocumentStore()
    {
        IDocumentStore documentStore = new DocumentStore
        {
            //Certificate = x509Certificate,
            Urls = new[] { "http://localhost:8080" },
            Database = "OpenFibu",
                
        };
        var myRevisionsConfiguration = new RevisionsConfiguration
        {
            Default = new RevisionsCollectionConfiguration
            {
                Disabled = false
            }
        };
        documentStore.Initialize();
        // var revisionsConfigurationOperation = new ConfigureRevisionsOperation(myRevisionsConfiguration);
        // Store.Maintenance.Send(revisionsConfigurationOperation);
        return documentStore;
    }

    public static IDocumentStore Store
    {
        get { return _store.Value; }
    }
}