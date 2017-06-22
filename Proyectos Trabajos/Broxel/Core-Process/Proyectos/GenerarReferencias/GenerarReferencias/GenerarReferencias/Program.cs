﻿namespace GenerarReferencias
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var lista = ObtenerRererecias();

            GenerarArchivoReferencia(lista);

            Console.WriteLine("Completo!");

            Console.ReadLine();
        }


        private static List<string> ObtenerRererecias()
        {
            string line;
            var bandera = false;

            var counter = 1;

            var listaReferenciaFinal = new List<string>();
            var listaReferencia2 = new List<string>();
            var listaComparacion = new List<string>();

            var file = new StreamReader(@"C:\Archivos\ReferenciasDiestel.txt");

            while (!bandera)
            {
                line = file.ReadLine();
                var bin = line.Substring(0, 6);
                var cuenta = line.Substring(6, 9);

                var referencia = GenerarReferencia(bin, cuenta, 2);
                var referencia2 = GenerarReferencia(bin, cuenta, 1);

                listaReferencia2.Add(referencia2);
                listaReferenciaFinal.Add(referencia);

                var comparacion = "Contador: " + string.Format("{0:00000}", counter) + "\t Referencia2: " + referencia2  + "\t Referencia3: " + line + "\t Referencia4: " + referencia + "\t( Cuenta: " + cuenta + ", Bin: " + bin + " )";

                listaComparacion.Add(comparacion);

                if (counter == 90000)
                    break;

                counter++;
            }

            var urlFile = @"C:\Archivos\Comparacion.txt";

            if (File.Exists(urlFile))
                File.Delete(urlFile);

            using (var sw = File.AppendText(urlFile))
            {
                foreach (var item in listaComparacion)
                {
                    sw.WriteLine(item);
                }

                sw.Close();
            }

            file.Close();

            return listaReferenciaFinal;
        }

        public static void GenerarArchivoReferencia(List<string> lista)
        {
            var urlFile = @"C:\Archivos\Referencias4.txt";

            if (File.Exists(urlFile))
                File.Delete(urlFile);

            using (var sw = File.AppendText(urlFile))
            {
                foreach (var item in lista)
                {
                    sw.WriteLine(item);
                }

                sw.Close();
            }
        }

        private static string GenerarReferencia(String bin, String cuenta, int tipoCodigo)
        {
            string numero = "";
            if (tipoCodigo == 1)
                numero = "060000" + cuenta + NumVerificadorOXXO("060000" + cuenta);
            else if (tipoCodigo == 2)
                numero = bin + cuenta + NumVerificadorLuhn(bin + cuenta);
            else if (tipoCodigo == 3)
                numero = cuenta;
            return numero;
        }

        internal static int NumVerificadorOXXO(string numero)
        {
            int sum = 0;
            bool isEven = true;
            List<char> arregloNumero = numero.ToCharArray().Reverse().ToList();
            foreach (char c in arregloNumero)
            {
                int temp = Convert.ToInt32(new string(c, 1));
                if (isEven)
                {
                    temp = temp * 2;
                    if (temp >= 10)
                    {
                        sum += temp % 10;
                        sum += temp / 10;
                    }
                    else sum += temp;
                }
                else
                {
                    sum += temp;
                }
                isEven = !isEven;
            }
            if (sum % 10 == 0)
                return 0;
            return (10 - (sum % 10));
        }


        static int NumVerificadorLuhn(string numero)
        {
            int sumOfDigits = numero.Where(e => e >= '0' && e <= '9').Reverse().Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2)).Sum(e => e / 10 + e % 10);
            return sumOfDigits % 10;

            return obtenerSumaPorDigitos(numero);
            var sum = 0;
            var alt = true;
            var digits = numero.ToCharArray();
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                var curDigit = (digits[i] - 48);
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                        curDigit -= 9;
                }
                sum += curDigit;
                alt = !alt;
            }
            if ((sum % 10) == 0)
            {
                return 0;
            }
            return (10 - (sum % 10));

        }

        static int obtenerSumaPorDigitos(String cadena)
        {
            int pivote = 2;
            int longitudCadena = cadena.Length;
            int cantidadTotal = 0;
            int b = 1;
            for (int i = 0; i < longitudCadena; i++)
            {
                if (pivote == 8)
                {
                    pivote = 2;
                }
                int temporal = Convert.ToInt32(cadena.Substring(i, b));
                b++;
                temporal *= pivote;
                pivote++;
                cantidadTotal += temporal;
            }
            cantidadTotal = 11 - cantidadTotal % 11;
            return cantidadTotal;
        }
    }
}
