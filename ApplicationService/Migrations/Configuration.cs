using System.Data.Entity.Migrations;

namespace ApplicationService.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PersistentStorage.EntityFramework.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
