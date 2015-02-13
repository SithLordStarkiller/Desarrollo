using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTask
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            new Program().Proceso();
            Console.ReadKey();
        }

        private void Proceso()
        {
            Task[] tasks = new Task[5];
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = Task<int>.Factory.StartNew(() => DoSomeWork(10000000));
                Console.WriteLine("Ejecutando Tarea {0}", i);
            }
            Task.WaitAll(tasks);

            foreach (var item in tasks)
            {
                var valor = ((Task<int>)item).Result;
                Console.WriteLine("El preceso {0} se completo en {1}", item.Id, valor);
            }
        }


        int DoSomeWork(int val)
        {
            // Pretend to do something
            var tiempo = new Random().Next(3000, 5000);
            Console.WriteLine("Tiempo Asignado: {0}",tiempo);
            Thread.Sleep(tiempo);
            return tiempo;
        }

        static double TrySolution1()
        {
            int i = rand.Next(1000000);
            // Simulate work by spinning
            Thread.SpinWait(i);
            return DateTime.Now.Millisecond;
        }
        static double TrySolution2()
        {
            int i = rand.Next(1000000);
            // Simulate work by spinning
            Thread.SpinWait(i);
            return DateTime.Now.Millisecond;
        }
        static double TrySolution3()
        {
            int i = rand.Next(1000000);
            // Simulate work by spinning
            Thread.SpinWait(i);
            Thread.SpinWait(1000000);
            return DateTime.Now.Millisecond;
        }
    }
}
