using System.Data.Entity.Migrations;

namespace ApplicationService.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationService.PersistentStorage.EntityFramework.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
