using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using ApplicationService.PersistentStorage;
using ApplicationService.PersistentStorage.EntityFramework;
using ApplicationService.PersistentStorage.NHibernate;
using Common;
using Common.Entities;

namespace ApplicationService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class ReadService : IReadService
    {
        private readonly IDbAbstraction _db = new EntityFrameworkImpl();

        public Task<IList<Item>> ReadAsync()
        {
            var items = _db.GetAll();
            return Task.FromResult(items);
        }
    }
}
