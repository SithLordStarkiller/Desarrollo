using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegados
{
    class Program
    {
        public delegate double Operacion(double num1, double num2); 
        static void Main(string[] args) 
        { 
            double num1 = 8; 
            double num2 = 6; 
            Operacion operacionSuma = new Operacion(Sumar); 
            double resultSuma = RealizarOperacion(num1, num2, operacionSuma); 
            Operacion operacionResta = new Operacion(Restar); 
            double resultResta = RealizarOperacion(num1, num2, operacionResta); 
            Console.WriteLine(string.Format("Suma: {0}", resultSuma)); 
            Console.WriteLine(string.Format("Resta: {0}", resultResta));
            Console.ReadLine();
        } 
        static double RealizarOperacion(double num1, double num2, Operacion operacion) 
        { 
            return operacion(num1, num2); 
        } 
        static double Sumar (double num1, double num2) 
        { 
            return num1 + num2; 
        }

        private static double Restar(double num1, double num2)
        {
            return num1 - num2;
        }
    }
}
