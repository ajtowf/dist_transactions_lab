using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Common.Entities;

namespace Common
{
    [ServiceContract]
    public interface IReadService
    {
        [OperationContract]
        Task<IList<Item>> ReadAsync();        
    }
}
