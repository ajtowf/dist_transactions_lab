using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace ApplicationService.PersistentStorage.NHibernate
{
    public class NHibernateImpl: IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                items = session.QueryOver<Item>().List().ToList();
                transaction.Commit();
            }

            return items;
        }

        public Item Write(Item item)
        {
            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                transaction.Commit();
            }

            return item;
        }
    }
}