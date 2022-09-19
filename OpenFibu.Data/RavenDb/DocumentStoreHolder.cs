using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace OpenFibu.Data.RavenDb
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> _store = new(CreateDocumentStore);

        private static IDocumentStore CreateDocumentStore()
        {
            string serverURL = "https://a.misdirectionr.ravendb.community:8080";
            string databaseName = "Finanzbuchhaltung";
            X509Certificate2 x509Certificate = new("C:\\Users\\misdi\\Desktop\\misdirectionr.Cluster.Settings\\admin.client.certificate.misdirectionr.pfx");
            IDocumentStore documentStore = new DocumentStore
            {
                Certificate = x509Certificate,
                Urls = new[] { serverURL },
                Database = databaseName
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
