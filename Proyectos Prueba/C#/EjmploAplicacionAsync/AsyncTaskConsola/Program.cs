using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTaskConsola
{
    public class EventoOperaciones : EventArgs
    {
        public int Suma;
        public bool SumaFinalizado = false;
        public int Resta;
        public bool RestaFinalizado = false;

        public delegate void EventoHandler(int a, int b);
        public event EventoHandler AlfinalizarOperaciones;

        public void IniciaSuma(int suma)
        {
            Suma = suma;
            SumaFinalizado = true;
        }

        public void IniciaResta(int resta)
        {
            Resta = resta;
            RestaFinalizado = true;
        }

        public void Finalizado()
        {
            if (SumaFinalizado && RestaFinalizado)
                AlfinalizarOperaciones(Suma, Resta);
        }
    }

    class Program
    {
        private static EventoOperaciones _evento;
        static void Main(string[] args)
        {
            new Program().Inicio();
        }

        public void Inicio()
        {
            _evento = new EventoOperaciones();
            _evento.AlfinalizarOperaciones += _evento_AlfinalizarOperaciones;

            Console.WriteLine("Inserta numero 1");
            var a = 42;

            Console.WriteLine("Inserta numero 2");
            var b = 40;

            new Program().CalcularSuma(a, b);
            Console.WriteLine("Se envio Suma");

            new Program().CalculaResta(a, b);
            Console.WriteLine("Se envio Resta");

            Console.ReadLine();
        }

        public Task<int> SumaAsync(int a, int b)
        {
            return Task.Run(() =>
            {
                int c;
                //Thread.Sleep(9000);
                c = Fibonacci(a);
                _evento.IniciaSuma(c);
                return c;
            });
        }

        public Task<int> RestaAsync(int a, int b)
        {
            return Task.Run(() =>
            {
                int c;
                //Thread.Sleep(6000);
                c = Fibonacci(b);
                _evento.IniciaResta(c);
                return c;
            });
        }
        static int Fibonacci(int x)
        {
            if (x <= 1)
                return 1;
            return Fibonacci(x - 1) + Fibonacci(x - 2);
        }

        public async void CalcularSuma(int a, int b)
        {
            var c = await SumaAsync(a, b);
            Console.WriteLine("Resultado suma {0}", c);
            _evento.Finalizado();
        }

        public async void CalculaResta(int a, int b)
        {
            var d = await RestaAsync(a, b);
            Console.WriteLine("Resultado Resta {0}", d);
            _evento.Finalizado();
        }

        public void _evento_AlfinalizarOperaciones(int a, int b)
        {
            Console.WriteLine("El producto de {0} por {1} es {2}", a, b, a * b);
            _evento.AlfinalizarOperaciones -= _evento_AlfinalizarOperaciones;
        }
    }
}
