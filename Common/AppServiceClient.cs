using System.Collections.Generic;
using System.ServiceModel;
using Common.Entities;

namespace Common
{
    public class AppServiceClient : ClientBase<IAppService>, IAppService
    {
        public IEnumerable<Item> Read(bool useEntityFramework = true)
        {
            return Channel.Read(useEntityFramework);
        }

        public void Write(bool useEntityFramework = true)
        {
            Channel.Write(useEntityFramework);
        }        
    }
}
