using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using System.Threading;
using System;
using System.Data;

namespace ApplicationService.PersistentStorage.EntityFramework
{
    public class EntityFrameworkImpl : IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var db = new AppDbContext())
            using(var transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                items = db.Items.ToList();
                transaction.Commit();
            }

            return items;
        }

        public Item Write(Item item)
        {
            using (var db = new AppDbContext())
            using (var transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    item = db.Items.Add(item);
                    db.SaveChanges();
                    Thread.Sleep(25 * 1000);
                    
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return item;
        }
    }
}