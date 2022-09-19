using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities;
using Raven.Client.Documents.Session;
using System.Linq.Expressions;

namespace OpenFibu.Data.RavenDb
{
    public class SteuerschluesselRepository : IRepository<Steuerschluessel>
    {
        public void Create(Steuerschluessel steuerschluessel)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();

            session.Store(steuerschluessel);
            session.SaveChanges();
        }

        public Steuerschluessel Get(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Steuerschluessel> GetAll()
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            IQueryable<Steuerschluessel> fullCollectionQuery = session.Query<Steuerschluessel>();

            return fullCollectionQuery.ToList();
        }

        public IEnumerable<Steuerschluessel> GetAll(Expression<Func<Steuerschluessel, bool>>? predicate = null)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            IQueryable<Steuerschluessel> fullCollectionQuery = session.Query<Steuerschluessel>().Where(predicate);

            return fullCollectionQuery.ToList();
        }

        public Steuerschluessel Get(Expression<Func<Steuerschluessel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Steuerschluessel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Steuerschluessel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Steuerschluessel entity)
        {
            throw new NotImplementedException();
        }
    }
}
