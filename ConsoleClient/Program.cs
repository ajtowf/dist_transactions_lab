using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Common;
using System.Transactions;

namespace ConsoleClient
{
    class Program
    {
        static void Main()
        {
            StartWriteThread();
            StartReadThread();
            Console.ReadKey();
        }

        private static void StartReadThread()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (var readProxy = new ReadServiceClient())
                        {
                            var items = await readProxy.ReadAsync();
                            Console.WriteLine($"Read {items.Count} items");
                        }
                    }
                    catch (FaultException<ExceptionDetail> e)
                    {
                        Console.WriteLine(e.Detail.InnerException.Message);
                        throw;
                    }
                }
            });
        }

        private static void StartWriteThread()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                        using (var proxy = new AppServiceClient())
                        {
                            await proxy.CallAsync();
                            scope.Complete();
                            
                        }
                    }
                    catch (FaultException<ExceptionDetail> e)
                    {
                        Console.WriteLine(e.Detail.InnerException.Message);
                        throw;
                    }

                    await Task.Delay(1000);
                }
            });
        }
    }
}
