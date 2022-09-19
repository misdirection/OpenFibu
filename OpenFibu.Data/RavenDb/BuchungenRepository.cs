using OpenFibu.Application.Interfaces;
using OpenFibu.Domain.Entities.Journal;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Linq.Expressions;

namespace OpenFibu.Data.RavenDb
{
    public class GeschaeftsvorfallRepository : IRepository<Geschaeftsvorfall>, IReadRepository<Geschaeftsvorfall>
    {
        public void Add(Geschaeftsvorfall entity)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();

            foreach (var buchung in entity.Buchungen)
                session.Store(buchung);
            session.Store(entity);
            session.SaveChanges();
        }

        public void Delete(Geschaeftsvorfall geschaeftsvorfall)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            session.Delete(geschaeftsvorfall);
            session.SaveChanges();
        }

        public Geschaeftsvorfall Get(Expression<Func<Geschaeftsvorfall, bool>> predicate)
        {
            using var session = DocumentStoreHolder.Store.OpenSession();
            IQueryable<Geschaeftsvorfall> queryByDocumentId = session.Query<Geschaeftsvorfall>()
                  .Where(predicate);

            return queryByDocumentId.FirstOrDefault();
        }

        public IEnumerable<Geschaeftsvorfall> GetAll(Expression<Func<Geschaeftsvorfall, bool>>? predicate = null)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            IQueryable<Geschaeftsvorfall> fullCollectionQuery = session.Query<Geschaeftsvorfall>().Include(gv => gv.Buchungen);

            return fullCollectionQuery.ToList();
        }

        public void Update(Geschaeftsvorfall entity)
        {
            //kp obs so geht
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            Geschaeftsvorfall gv = session.Load<Geschaeftsvorfall>(entity.Id);
            gv = entity;
            session.SaveChanges();
        }

        public Task<IEnumerable<Geschaeftsvorfall>> FindByConditionAsync(Expression<Func<Geschaeftsvorfall, bool>> expression, params Expression<Func<Geschaeftsvorfall, object>>[] includeProperties)
        {
            using IDocumentSession session = DocumentStoreHolder.Store.OpenSession();
            var query = session.Query<Geschaeftsvorfall>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            var list = query.Where(expression);

            return Task.FromResult((IEnumerable<Geschaeftsvorfall>)list.ToList());
        }

    }
}
