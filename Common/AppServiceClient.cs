using System.ServiceModel;
using System.Threading.Tasks;

namespace Common
{
    public class AppServiceClient : ClientBase<IAppService>, IAppService
    {
        public Task CallAsync()
        {
            return Channel.CallAsync();
        }

        public Task WriteAsync()
        {
            return Channel.WriteAsync();
        }
    }
}
