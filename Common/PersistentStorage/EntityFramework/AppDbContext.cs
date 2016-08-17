using System.Data.Entity;
using Common.Entities;

namespace Common.PersistentStorage.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base(@"Server=.\;Database=disttranslab;Integrated Security=True;MultipleActiveResultSets=true;")
        {
        }
        public DbSet<Item> Items { get; set; }
    }
}