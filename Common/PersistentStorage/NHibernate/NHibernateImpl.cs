using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using System.Threading;
using System;
using System.Data;

namespace Common.PersistentStorage.NHibernate
{
    public class NHibernateImpl : IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                items = session.QueryOver<Item>().List().ToList();
                transaction.Commit();
            }

            return items;
        }

        public Item Write(Item item)
        {
            if (System.Transactions.Transaction.Current.TransactionInformation.Status == System.Transactions.TransactionStatus.Aborted)
            {
                System.Transactions.Transaction.Current.Dispose();
                System.Transactions.Transaction.Current = null;
            }

            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    session.SaveOrUpdate(item);
                    Thread.Sleep(25 * 1000);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    if (session.Transaction.IsActive)
                        session.Transaction.Rollback();
                    
                    // Detta orsakar fel!!!!!!!!!! Rollback på session.Transaction istället!
                    //if (transaction.IsActive)
                    //    transaction.Rollback();

                    throw;
                }
            }


            return item;
        }
    }
}