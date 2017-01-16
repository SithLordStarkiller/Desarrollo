using System;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace PubliPayments.Utiles
{
    public class Security
    {
        public static string HashSHA512(string value)
        {
            if (value == null)
                return null;
            var encoder = new UTF8Encoding();
            var sha512Hasher = new SHA512CryptoServiceProvider();
            var hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(value));
            return ByteArrayToHexString(hashedDataBytes);
        }

        internal static string ByteArrayToHexString(byte[] inputArray)
        {
            if (inputArray == null)
                return null;
            var o = new StringBuilder("");
            foreach (byte t in inputArray)
                o.Append(t.ToString("X2"));
            return o.ToString();
        }
        /// <summary>
        ///Genera un string de caracteres aleatorios de acuerdo a las condiciones que se presenten. MA-mayúscula, CE-caracterEspecial, NU-números, MI-minúscula, NA-cualquier tipo anterior ej: {{ "1", "MA" }, { "1", "CE" }}
        /// </summary>
        /// <param name="arr"> condición para generar el password [No caracteres necesarios, tipo de caracter]</param>
        /// <returns> array [0],[1] 0- password resultante , 1- cadena encriptada sha512</returns>
        /// JARO
        public static string[] GeneratePassWord(string[,] arr)
        {
            var rdm = new Random();
            var response = new ArrayList();
            string[] lM =
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S",
                "T", "U", "V", "W", "X", "Y", "Z"
            };
            string[] cE =
            {
                "!", "#", "$", "%", "&", "(", ")", "*", "+", "-", ".", "?", "@", "[", "]", "_"
            };

            for (int x = 0; x < arr.GetLength(0); x++)
            {
                int required = Int32.Parse(arr[x, 0]);
                switch (arr[x, 1])
                {
                    case "MA": //mayúscula
                        do
                        {
                            response.Add(lM[rdm.Next(lM.Length - 1)]);
                            required--;
                        } while (required > 0);
                        break;
                    case "CE"://caracterEspecial
                        do
                        {
                            response.Add(cE[rdm.Next(cE.Length - 1)]);
                            required--;
                        } while (required > 0);
                        break;
                    case "NU"://números
                        do
                        {
                            response.Add(rdm.Next(9).ToString(CultureInfo.InvariantCulture));
                            required--;
                        } while (required > 0);
                        break;
                    case "MI"://minúscula
                        do
                        {
                            response.Add(lM[rdm.Next(lM.Length - 1)].ToLower());
                            required--;
                        } while (required > 0);
                        break;
                    case "NA": //variado
                        do
                        {
                            int aleat = rdm.Next(lM.Length - 1);
                            response.Add((aleat % 2 == 0)
                                ? (aleat % 3 == 0) ? rdm.Next(9).ToString(CultureInfo.InvariantCulture) : lM[aleat]
                                : lM[aleat].ToLower());
                            required--;
                        } while (required > 0);
                        break;
                }
            }
            var password = "";
            while (response.Count > 0)
            {
                int val = rdm.Next(0, response.Count - 1);
                password += (response[val]);
                response.RemoveAt(val);
            }
            return new[] { password, HashSHA512(password) };
        }
    }
}
