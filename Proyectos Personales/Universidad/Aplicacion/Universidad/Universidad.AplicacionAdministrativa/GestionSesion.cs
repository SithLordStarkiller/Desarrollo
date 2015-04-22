using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Universidad.Entidades.ControlUsuario;
using Universidad.Controlador.Controlador;

namespace Universidad.AplicacionAdministrativa
{
    public class GestionSesion
    {
        private Sesion _sesion;
        private readonly string _ruta = Directory.GetCurrentDirectory() + @"\\Configuracion\\";
        private const string Archivo = @"Sesion.config";

        public Sesion ExisteSesion()
        {
            if (Directory.Exists(_ruta))
            {
                if (!File.Exists(_ruta + Archivo))
                    return CreaArchivo() ? _sesion : null;

                _sesion = LeeArchivo();

                return _sesion;
            }
            if (CreaCarpeta())
            {
                return CreaArchivo() ? _sesion : null;
            }

            return null;
        }

        private bool CreaCarpeta()
        {
            try
            {
                Directory.CreateDirectory(_ruta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CreaArchivo()
        {
            _sesion = new Sesion();
            TextWriter tw = new StreamWriter(_ruta + Archivo);
            tw.WriteLine(TratamientoEscritura(_sesion));
            tw.Close();
            return true;
        }

        private Sesion LeeArchivo()
        {
            using (var sr = new StreamReader(_ruta + Archivo))
            {
                var line = sr.ReadToEnd();
                _sesion = TratamentoLectura(line);
                return _sesion;
            }
        }

        public void ActualizaArchivo(Sesion sesion)
        {
            TextWriter tw = new StreamWriter(_ruta + Archivo);
            tw.WriteLine(TratamientoEscritura(sesion));
            tw.Close();
        }

        public string TratamientoEscritura(Sesion sesion)
        {
            var cadena = SerializacionXml.SerializeToXml(sesion);
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

        public Sesion TratamentoLectura(string textoEncriptado)
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

            _sesion = new Sesion();

            var sesion = (Sesion)SerializacionXml.DeserializeTo(resultado,_sesion);

            return sesion;
        }
    }
}
