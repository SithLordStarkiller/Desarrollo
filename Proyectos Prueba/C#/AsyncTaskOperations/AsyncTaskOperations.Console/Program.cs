using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTaskOperations.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            var cTokenSource = new CancellationTokenSource();

            var cToken = cTokenSource.Token;

            var t1 = Task.Factory.StartNew(() => GenerateNumbers(cToken), cToken);            

            Console.WriteLine("Press 1 to cancel task");

            cToken.Register(() => cancelNotification());

            if (Console.ReadKey().KeyChar == '1')
            {
                cTokenSource.Cancel();
                
            }
            Console.ReadLine();
        }

        void Proceso()
        {

        }

        void GenerateNumbers(CancellationToken ct)
        {
            int i;
            for (i = 0; i < 10; i++)
            {
                Console.WriteLine("Method - Number: {0} \n", i);
                Thread.Sleep(1000);

                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        static void cancelNotification()
        {
            Console.WriteLine("\n\nCancellation request made!!");
        }
    }
}
