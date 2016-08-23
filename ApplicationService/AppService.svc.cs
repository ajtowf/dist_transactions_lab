using System.ServiceModel;
using Common;
using Common.Entities;
using System.Collections.Generic;
using IsolationLevel = System.Data.IsolationLevel;
using System;
using System.Threading;
using System.Linq;
using log4net;

namespace ApplicationService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]    
    public class AppService : IAppService
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void Write(int operation)
        {
            //var logger = LogManager.GetLogger(typeof(AppService));
            //logger.Debug("Hej Darko!!");

            using (var context = new DatabaseContext(IsolationLevel.ReadCommitted))
            {
                context.Session.SaveOrUpdate(Item.Create);

                switch (operation)
                {
                    case 1:
                        throw new Exception();
                    case 2:
                        Thread.Sleep(25 * 1000);
                        break;
                    default:
                        break;
                }

                context.Commit();
            }
        }

        public IEnumerable<Item> Read()
        {
            IList<Item> items;
            using (var context = new DatabaseContext(IsolationLevel.ReadCommitted))
            {
                items = context.Session.QueryOver<Item>().List().ToList();
                context.Commit();
            }

            return items;
        }
    }
}
