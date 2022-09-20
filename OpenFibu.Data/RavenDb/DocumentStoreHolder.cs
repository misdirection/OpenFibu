using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace OpenFibu.Data.RavenDb
{
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

            documentStore.Initialize();
            return documentStore;
        }

        public static IDocumentStore Store
        {
            get { return _store.Value; }
        }
    }
}
