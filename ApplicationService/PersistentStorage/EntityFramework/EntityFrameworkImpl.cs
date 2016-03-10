using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace ApplicationService.PersistentStorage.EntityFramework
{
    public class EntityFrameworkImpl : IDbAbstraction
    {
        public IList<Item> GetAll()
        {
            IList<Item> items;
            using (var db = new AppDbContext())
            {
                items = db.Items.ToList();
            }

            return items;
        }

        public Item Write(Item item)
        {
            using (var db = new AppDbContext())
            {
                item = db.Items.Add(item);
                db.SaveChanges();
            }

            return item;
        }
    }
}