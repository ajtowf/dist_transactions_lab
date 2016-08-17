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
            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                session.SaveOrUpdate(item);
                Thread.Sleep(25 * 1000);
                transaction.Commit();

                ////try
                ////{
                ////    session.SaveOrUpdate(item);
                ////    Thread.Sleep(25 * 1000);
                ////    transaction.Commit();
                ////    //if (System.Transactions.Transaction.Current.TransactionInformation.Status == System.Transactions.TransactionStatus.Active)
                ////    transaction.Commit();
                ////    //else
                ////    //    throw new Exception("Timed out!");
                ////}
                ////catch
                ////{
                ////    //// When timed out this one is active, failing to rollback
                ////    //if (transaction.IsActive)
                ////    //    transaction.Rollback();

                ////    //// this one is inactive, hence no failed rollback
                ////    //// which should we do? If any at all?
                ////    //if (session.Transaction.IsActive)
                ////    //    session.Transaction.Rollback();

                ////    throw;
                ////}
                ////finally
                ////{
                ////    session.Close();
                ////}
            }


            return item;
        }
    }
}