using System.ServiceModel;
using System.Transactions;
using Common.PersistentStorage.NHibernate;
using Common;
using Common.Entities;
using System.Collections.Generic;
using Common.PersistentStorage;
using Common.PersistentStorage.EntityFramework;

namespace ApplicationService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]    
    public class AppService : IAppService
    {
        private readonly IDbAbstraction _nh = new NHibernateImpl();
        private readonly IDbAbstraction _ef = new EntityFrameworkImpl();

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Write(bool useEntityFramework = true)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                if (useEntityFramework)
                    _ef.Write(Item.Create);
                else
                    _nh.Write(Item.Create);

                scope.Complete();
            }
        }

        public IEnumerable<Item> Read(bool useEntityFramework = true)
        {
            return useEntityFramework ? _ef.GetAll() : _nh.GetAll();            
        }
    }
}
