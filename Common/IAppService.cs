using Common.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IAppService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void Write(bool useEntityFramework = true);

        [OperationContract]        
        IEnumerable<Item> Read(bool useEntityFramework = true);
    }
}
