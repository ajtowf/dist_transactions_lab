using System;
using Common;
using System.Transactions;
using System.ServiceModel;

namespace ConsoleClient
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Running...");
            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Escape)
            {
                try
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("Write with NH");
                            Write(false);
                            break;
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Read with NH");
                            Read(false);
                            break;
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("Write with EF");
                            Write();
                            break;
                        case ConsoleKey.NumPad4:
                            Console.WriteLine("Read with EF");
                            Read();
                            break;                        
                        default:
                            break;
                    }
                }
                catch (FaultException<ExceptionDetail> exception)
                {
                    Console.WriteLine($"Exception: {exception.Detail.Message}");
                    Console.WriteLine($"Inner exception: {exception.Detail?.InnerException?.Message}");
                }

                Console.WriteLine("Waiting for new key press...");
                key = Console.ReadKey();
                Console.Clear();
            }
        }

        private static void Write(bool useEntityFramework = true)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromSeconds(20)
                }))
            using (var proxy = new AppServiceClient())
            {
                proxy.Write(useEntityFramework);
                scope.Complete();
            }
        }

        private static void Read(bool useEntityFramework = true)
        {
            using (var proxy = new AppServiceClient())
            {
                proxy.Read(useEntityFramework);
            }
        }
    }
}
