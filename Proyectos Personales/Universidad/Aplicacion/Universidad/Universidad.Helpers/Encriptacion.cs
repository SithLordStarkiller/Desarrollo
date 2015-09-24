using System;
using System.Security.Cryptography;
using System.Text;

namespace Universidad.Helpers
{
    public class Encriptacion
    {
        public string EncriptarTexto(string cadena)
        {
            try
            {
                var arregloCifrar = Encoding.UTF8.GetBytes(cadena);
                var hashmd5 = new MD5CryptoServiceProvider();
                var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("password"));

                hashmd5.Clear();

                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateEncryptor();
                var arrayResultado = cTransform.TransformFinalBlock(arregloCifrar, 0, arregloCifrar.Length);

                tdes.Clear();

                var encriptado = Convert.ToBase64String(arrayResultado, 0, arrayResultado.Length);

                return encriptado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string DesencriptarTexto(string textoEncriptado)
        {

            var arrayDescifrar = Convert.FromBase64String(textoEncriptado);

            var hashmd5 = new MD5CryptoServiceProvider();

            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes("password"));

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();

            var resultArray = cTransform.TransformFinalBlock(arrayDescifrar, 0, arrayDescifrar.Length);

            tdes.Clear();

            var resultado = Encoding.UTF8.GetString(resultArray);


            return resultado;
        }
    }
}
