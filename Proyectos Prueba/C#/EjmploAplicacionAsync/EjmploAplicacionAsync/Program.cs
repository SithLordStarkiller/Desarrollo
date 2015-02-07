using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploAplicacionAsync
{
    static class Program
    {
        static void Main()
        {
            System.Threading.Thread tarea =
                new System.Threading.Thread(
                    new System.Threading.ParameterizedThreadStart(TareaLenta));
            //Se inicia la otra tarea
            tarea.Start(900);
            //Se revisa si ya terminó la otra tarea
            if (tarea.ThreadState != System.Threading.ThreadState.Stopped)
                System.Console.WriteLine("La otra tarea sigue corriendo");
            //Se detiene el hilo actual hasta que termine la otra tarea o
            //hasta que pasen 1000 milisegundos 
            tarea.Join(100000);
            //Se revisa otra vez si ya terminó la otra tarea
            if (tarea.ThreadState != System.Threading.ThreadState.Stopped)
                System.Console.WriteLine("Paso un segundo y la otra tarea sigue...");
            else
                System.Console.WriteLine("¡Por fin terminó la otra tarea!");
            System.Console.ReadLine();
        }
        static void TareaLenta(object parametro)
        {
            //Parámetro en forma de entero
            int lapso = (int)parametro;
            //Deteniendo la tarea por el tiempo indicado
            System.Threading.Thread.Sleep(lapso);
            System.Console.WriteLine("Esta tarea lenta ha terminado");
        }
    }
}
