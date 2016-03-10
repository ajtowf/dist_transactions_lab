using System.ServiceModel;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAppService
    {
        [OperationContract, TransactionFlow(TransactionFlowOption.Allowed)]
        Task CallAsync();

        [OperationContract, TransactionFlow(TransactionFlowOption.Allowed)]
        Task WriteAsync();        
    }
}
