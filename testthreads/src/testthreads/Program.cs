using System;
using System.Threading;
using System.Threading.Tasks;

namespace testthreads
{
    public class Program
    {
        static readonly Mutex mutex = new Mutex(false, "Mutex_Test");
        
        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(AquireMutex);
                thread.Start($"Thread {i+1}");
            }   
        }

        private static void AquireMutex(object threadName)
        {
            if (!mutex.WaitOne(1000))
            {
                Console.WriteLine($"Mutex COULD NOT BE aquired by {threadName}");
                return;
            }

            DoSomething();
            Console.WriteLine($"Mutex aquired by {threadName}");
            mutex.ReleaseMutex();
            Console.WriteLine($"Mutex released by {threadName}");
        }

        private static void DoSomething()
        {
            Thread.Sleep(500);
        }
    }
}
