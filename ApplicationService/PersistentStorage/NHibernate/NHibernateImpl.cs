using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using System.Threading;
using System;
using System.Data;
using NHibernate;

namespace ApplicationService.PersistentStorage.NHibernate
{
    

    public class NHibernateImpl : IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var context = new DatabaseContext(IsolationLevel.ReadCommitted))
            {
                items = context.Session.QueryOver<Item>().List().ToList();
                context.Commit();
            }

            return items;
        }

        public Item Write(Item item)
        {
            using (var context = new DatabaseContext(IsolationLevel.ReadCommitted))
            {
                context.Session.SaveOrUpdate(item);
                //throw new Exception();
                Thread.Sleep(25 * 1000);
                context.Commit();
            }

            return item;
        }
    }
}