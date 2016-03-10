using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Common.Entities;

namespace Common
{
    public class ReadServiceClient : ClientBase<IReadService>, IReadService
    {
        public Task<IList<Item>> ReadAsync()
        {
            return Channel.ReadAsync();
        }
    }
}
