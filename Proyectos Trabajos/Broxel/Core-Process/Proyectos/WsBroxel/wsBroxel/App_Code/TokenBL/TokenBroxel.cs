using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.TokenBL
{
    public class TokenBroxel
    {
        /// <summary>
        /// Genera una semilla de 6 caracteres
        /// </summary>
        /// <returns>Semilla aleatoria de seis caracteres</returns>
        public static string GeneraSemilla()
        {
            var semilla = "";
            var r = new Random();
            var cSemilla = ' ';
            for (var i = 0; i < 6; i++)
            {
                do
                {
                    cSemilla = (char)r.Next(125);
                } while (!Filter(cSemilla));
                semilla += cSemilla;
            }
            return semilla;
        }
        /// <summary>
        /// Filtra caracteres numericos o especiales 
        /// </summary>
        /// <param name="a">Caracter a filtrar</param>
        /// <returns>True si el caracter es válido, false si es inválido</returns>
        public static bool Filter(char a)
        {
            var res = false;
            switch ((int)a)
            {
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                //case 73: ignora la I
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 97:
                case 98:
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                //case 108: Ignora la l
                case 109:
                case 110:
                case 111:
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                    res = true;
                    break;
            }
            return res;
        }
        /// <summary>
        /// Calibra Token
        /// </summary>
        /// <param name="token">Id de Token</param>
        /// <param name="semilla">primera semilla generada para validar en un intervalo definido</param>
        /// <param name="intervalo">Intervalo de busqueda para calibración, en minutos</param>
        /// <returns>Numero de minutos de diferencia entre el dispositivo y el servidor</returns>
        public static int CalibraToken(string token, string semilla, long intervalo)
        {
            var hoy = DateTime.Now;
            var calcOffset = 0;
            try
            {
                
                var prob = hoy.AddMinutes(0);
                var tokenChallenge = GeneraNumero(semilla, prob);
                if (token == tokenChallenge)
                {
                    return 0;
                }
                
                for (var i = 1; i < intervalo; i++)
                {
                    var offsetmin = i * (-1);
                    var offsetmax = i;
                    prob = hoy.AddMinutes(offsetmin);
                    tokenChallenge = GeneraNumero(semilla, prob);
                    if (token == tokenChallenge)
                    {
                        calcOffset = offsetmin;
                        break;
                    }
                    prob = hoy.AddMinutes(offsetmax);
                    tokenChallenge = GeneraNumero(semilla, prob);
                    if (token == tokenChallenge)
                    {
                        calcOffset = offsetmax;
                        break;
                    }
                }
            }
            catch
            {
                calcOffset = 0;
            }
            return calcOffset;
        }
        /// <summary>
        /// Valida Token entregado
        /// </summary>
        /// <param name="token">Token entregado</param>
        /// <param name="semilla">Semilla perteneciente al token</param>
        /// <param name="intervalo">Intervalo de gracia para el token, en minutos</param>
        /// <param name="offset">Diferencia de minutos entre el dispositivo y el servidor, 0 si estan en la misma zona horaria</param>
        /// <returns>True si el Token es válido, False si no lo es</returns>
        public static bool ValidaToken(string token, string semilla, int intervalo, long offset)
        {
            var resultado = false;
            try
            {
                var hoy = DateTime.Now;
                for (var i = intervalo * (-1); i <= intervalo; i++)
                {
                    var hoy2 = hoy.AddMinutes(i + offset);
                    var tokenChallenge = GeneraNumero(semilla, hoy2);
                    if (token != tokenChallenge) continue;
                    resultado = true;
                    break;
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }

        /// <summary>
        /// Genera Numero
        /// </summary>
        /// <param name="semilla">Semilla para generación de Token</param>
        /// <param name="fecha">Fecha para generacion de Token</param>
        /// <returns>Cadena de caracteres numerica en base a una semilla de seis caracteres</returns>
        public static string GeneraNumero(string semilla, DateTime fecha)
        {
            var numero = "";
            try
            {
                //Primero obtenemos el numero identificador del momento del año sin ofuscar
                // Numero del momento en el año = minuto[1-60] * hora [1-24] * diadelaño [1-366]
                var moment = (Vapulea((fecha.Minute + 1) * (fecha.Hour + 1) * fecha.DayOfYear, semilla)).ToString("D6");

                // Efectuar la operacion de corrimiento con cada digito de la semilla
                var i = 5;
                var j = 0;
                foreach (var caracter in moment)
                {
                    numero += GetNumber(caracter, semilla[i], j);
                    i--;
                    j++;
                }
            }
            catch
            {
                numero = "";
            }
            return numero;
        }
        /// <summary>
        /// Algoritmo de generación de numeros aleatorios en base a una posición de una cadena dada
        /// </summary>
        /// <param name="number">Numero</param>
        /// <param name="semilla">Cadena de semilla</param>
        /// <returns>Numero aleatorio de seis caracteres en base a un numero base y una semilla</returns>
        private static long Vapulea(int number, string semilla)
        {
            long resultado = number * semilla[0];
            resultado *= semilla[2];
            resultado += semilla[5];
            resultado -= semilla[3];
            if (resultado > 999999)
            {
                var res = resultado.ToString(CultureInfo.InvariantCulture);
                if (resultado % semilla[1] == 0)
                    resultado = Convert.ToInt32(res.Substring(0, 6));
                else
                    resultado = Convert.ToInt32(res.Substring(res.Length - 6));
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene un numero como char 
        /// </summary>
        /// <param name="cOri">char Original</param>
        /// <param name="semilla">semilla de origen</param>
        /// <param name="pos">posicion del bite (Most Significant o Less Significant)</param>
        /// <returns>Un numero como char</returns>

        private static char GetNumber(char cOri, char semilla, int pos)
        {
            switch (pos)
            {
                case 0:
                    return CorrimientoLsb(cOri, semilla, true);
                case 1:
                    return CorrimientoLsb(cOri, semilla, false);
                case 2:
                    return CorrimientoLsb(cOri, semilla, false);
                case 3:
                    return CorrimientoLsb(cOri, semilla, true);
                case 4:
                    return CorrimientoLsb(cOri, semilla, false);
                case 5:
                    return CorrimientoLsb(cOri, semilla, true);
                default:
                    return CorrimientoLsb(cOri, semilla, false);
            }

        }
        /// <summary>
        /// Obtiene un corrimiento de bits para un caracter en base a una semilla y un indicador
        /// </summary>
        /// <param name="cOri">char Original</param>
        /// <param name="semilla">semilla de origen</param>
        /// <param name="sentido">posicion del bite (Most Significant o Less Significant)</param>
        /// <returns></returns>
        private static char CorrimientoLsb(char cOri, char semilla, bool sentido)
        {
            var corr = sentido ? Math.Abs((int)cOri + (int)semilla).ToString(CultureInfo.InvariantCulture) : Math.Abs((int)cOri - (int)semilla).ToString(CultureInfo.InvariantCulture);
            return corr[corr.Length - 1];
        }

    }
}