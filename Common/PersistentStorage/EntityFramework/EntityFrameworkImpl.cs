using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using System.Threading;

namespace Common.PersistentStorage.EntityFramework
{
    public class EntityFrameworkImpl : IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var db = new AppDbContext())
            using(var transaction = db.Database.BeginTransaction())
            {
                items = db.Items.ToList();
                transaction.Commit();
            }

            return items;
        }

        public Item Write(Item item)
        {
            using (var db = new AppDbContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                item = db.Items.Add(item);
                db.SaveChanges();
                Thread.Sleep(25 * 1000);
                transaction.Commit();
            }

            return item;
        }
    }
}