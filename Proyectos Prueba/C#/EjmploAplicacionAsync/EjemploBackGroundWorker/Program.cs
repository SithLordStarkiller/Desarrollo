using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploBackGroundWorker
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
            //Se crea un objeto BackgroundWorker
            System.ComponentModel.BackgroundWorker tarea =
                new System.ComponentModel.BackgroundWorker();
            //Se genera el código que será asignado 
            //al evento DoWork (la tarea asíncrona)
            tarea.DoWork += (o, parametros) =>
            {
                //Se simula un retardo de 10 segundo 
                //Por ejemplo, un acceso muy tardado 
                //a una base de datos
                System.Threading.Thread.Sleep(10000);
                //En el resultado ponemos cualquier cosa
                //por ejemplo la hora actual
                parametros.Result = System.DateTime.Now;
            };
            // Se recupera el resultado al completar la tarea
            tarea.RunWorkerCompleted += (o, parametros) =>
            {
                if (parametros.Error != null)
                {
//Pon mensaje de error
                    System.Console.WriteLine("Con errores");
                }
                else
                {
//Pon mensaje de éxito
                    System.Console.WriteLine("Sin errores");
                    //Se obtiene el resultado obtenido en la tarea
                    //y se imprime
                    System.Console.WriteLine(parametros.Result.ToString());
                }


            };
            //Se pone a funcionar la tarea
            tarea.RunWorkerAsync();
            Console.WriteLine("Fin");
            System.Console.ReadLine();
        }
    }
}
