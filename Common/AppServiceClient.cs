using System.Collections.Generic;
using System.ServiceModel;
using Common.Entities;

namespace Common
{
    public class AppServiceClient : ClientBase<IAppService>, IAppService
    {
        public IEnumerable<Item> Read()
        {
            return Channel.Read();
        }

        public void Write(int operation)
        {
            Channel.Write(operation);
        }        
    }
}
