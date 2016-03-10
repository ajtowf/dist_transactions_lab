using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Transactions;
using ApplicationService.PersistentStorage;
using ApplicationService.PersistentStorage.EntityFramework;
using ApplicationService.PersistentStorage.NHibernate;
using Common;
using Common.Entities;

namespace ApplicationService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class AppService : IAppService
    {
        private readonly AppServiceClient _service;
        private readonly IDbAbstraction _db;

        public AppService()
        {
            _service = new AppServiceClient();
            _db = new EntityFrameworkImpl();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public async Task CallAsync()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _service.WriteAsync();                
                scope.Complete();
            }
        }
        
        [OperationBehavior(TransactionScopeRequired = true)]
        public Task WriteAsync()
        {
            //await WriteWithHibernateData();
            return Task.FromResult(_db.Write(Item.Create));
        }

        private static async Task WriteWithHibernateData()
        {
            using (var session = SessionManager.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                for (int i = 0; i < 3; i++)
                {
                    session.SaveOrUpdate(Item.Create);
                    await Task.Delay(100);
                }

                transaction.Commit();
            }
        }
    }
}
