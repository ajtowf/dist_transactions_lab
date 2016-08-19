using System;
using Common;
using System.Transactions;
using System.ServiceModel;
using System.Linq;

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
                            Console.WriteLine("Write.");
                            Write(0);
                            break;
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Write and throw.");
                            Write(1);
                            break;
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("Write and sleep.");
                            Write(2);
                            break;
                        case ConsoleKey.NumPad4:
                            Console.WriteLine("Read.");
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

        private static void Write(int operation)
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
                proxy.Write(operation);                
                scope.Complete();
            }
        }

        private static void Read()
        {
            using (var proxy = new AppServiceClient())
            {
                var items = proxy.Read().ToList();
                Console.WriteLine($"Read {items.Count} items.");
            }
        }
    }
}
