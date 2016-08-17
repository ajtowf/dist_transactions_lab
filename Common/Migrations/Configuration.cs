using System.Data.Entity.Migrations;

namespace Common.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Common.PersistentStorage.EntityFramework.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
