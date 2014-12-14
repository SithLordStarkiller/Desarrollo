using System;
using System.IO;
using Newtonsoft.Json;
using Universidad.Entidades.ControlUsuario;
using System.Xml;

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
                if (File.Exists(_ruta + Archivo))
                {
                    _sesion = LeeArchivo();
                    return _sesion;
                }
                else
                {
                    return CreaArchivo() ? _sesion : null;
                }
            }
            else
            {
                if (CreaCarpeta())
                {
                    return CreaArchivo() ? _sesion : null;
                }
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
            TextWriter tw = new StreamWriter(_ruta + Archivo);
            tw.WriteLine(JsonConvert.SerializeObject(_sesion));
            tw.Close();
            return true;
        }

        private Sesion LeeArchivo()
        {
            using (var sr = new StreamReader(_ruta + Archivo))
            {
                var line = sr.ReadToEnd();
                _sesion = JsonConvert.DeserializeObject<Sesion>(line);
                return _sesion;
            }
        }

        public void ActualizaArchivo(Sesion sesion)
        {
            TextWriter tw = new StreamWriter(_ruta + Archivo);
            tw.WriteLine(JsonConvert.SerializeObject(sesion));
            tw.Close();
        }
    }
}
