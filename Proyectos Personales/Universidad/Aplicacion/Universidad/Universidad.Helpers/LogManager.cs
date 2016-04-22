namespace Universidad.Helpers
{
    using System;
    using System.IO;

    public enum TipoLog
    {
        Informacion, Debug, Excepcion, ModificacionRegistro
    }

    public enum TipoAlmacenamiento
    {
        Archivo, BaseDeDatos
    }

    public class LogManager
    {
        private string NombreCompletoDelArchivo;
        private string nombreArchivo;
        private string FechaArchivo;
        private string Extencion;
        private string Archivo;
        //private readonly string _ruta = @"C:\inetpub\Publicaciones\Universidad\ServidorInterno\Logs\";
        private readonly string _ruta = @"C:\Desarrollo\Proyectos\Proyectos Personales\Universidad\Aplicacion\Universidad\Universidad.ServidorInterno\Logs\";

        public LogManager()
        {
            Archivo = "LogUniversidad";
            FechaArchivo = DateTime.Now.ToShortDateString().Replace("/","");
            Extencion = ".Log";

            nombreArchivo = Archivo + FechaArchivo + Extencion;
            NombreCompletoDelArchivo = _ruta + Archivo + FechaArchivo + Extencion;
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
            var fs = new FileStream(NombreCompletoDelArchivo, FileMode.Create);

            using (StreamWriter tw = new StreamWriter(fs))
            {
                var cabezera = "Fecha de creacion: " + DateTime.Now.ToShortDateString() + "\n Logs Universidad \n";
                tw.WriteLine(cabezera);
                tw.Close();
            }

            return true;
        }

        private bool ExisteArchivoLog()
        {
            var existeCarpeta = Directory.Exists(_ruta);

            if (!existeCarpeta)
            {
                CreaCarpeta();
            }
            else
            {
                if (File.Exists(NombreCompletoDelArchivo))
                {
                    var fechaArchivo = DateTime.Now;

                    using (var reader = new StreamReader(NombreCompletoDelArchivo))
                    {
                        var cabezera = reader.ReadLine();

                        cabezera = cabezera.Replace("Fecha de creacion: ", "");
                        cabezera = cabezera.Replace(" Logs Universidad ", "");

                        fechaArchivo = Convert.ToDateTime(cabezera);

                        if (fechaArchivo == DateTime.Today)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                else
                {
                    CreaArchivo();
                    return true;
                }
            }

            return false;
        }

        private string LeeArchivoLog()
        {
            using (var sr = new StreamReader(NombreCompletoDelArchivo))
            {
                return sr.ReadToEnd();
            }
        }

        private void EscribeArchivoLog(string registro)
        {
            var existeArchivo = ExisteArchivoLog();

            string text = "";

            if (existeArchivo)
            {
                text = LeeArchivoLog();

                TextWriter tw = new StreamWriter(NombreCompletoDelArchivo);
                                            
                tw.WriteLine(text + registro);
                tw.Close();
            }
            else
            {
                CreaArchivo();

                text = LeeArchivoLog();

                TextWriter tw = new StreamWriter(NombreCompletoDelArchivo);

                tw.WriteLine(text + registro);
                tw.Close();
            }
        }

        public void RegistraExcepcion(Exception e, string clase, string metodo, string aplicacion)
        {
            var texto = Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            texto += "Excepcion --> Clase: " + clase + " Metodo: " + metodo + " Hora: " + DateTime.Now + Environment.NewLine + "Nombre de Aplicacion: " + aplicacion + Environment.NewLine;
            texto += "Error: " + e.Message + Environment.NewLine;
            texto += "Descripcion: " + e.InnerException + Environment.NewLine;
            texto += "StackTrace: " + e.StackTrace + Environment.NewLine;
            texto += "Source: " + e.StackTrace + Environment.NewLine;
            texto += "NoException: " + e.HResult + Environment.NewLine;
            texto += "Help: " + e.HelpLink + Environment.NewLine;

            texto += Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            EscribeArchivoLog(texto);
        }

        public void RegistraModificacionBaseDeDatos(string clase, string metodo, string aplicacion, string informacion, string NombreTabla, string IdRegistro, string idUsuario, string idLogXml)
        {
            var texto = Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            texto += "Modificacion de registro --> Clase: " + clase + " Metodo: " + metodo + " Hora: " + DateTime.Now + Environment.NewLine + "Nombre de Aplicacion: " + aplicacion + Environment.NewLine;
            texto += "Informe: " + informacion + Environment.NewLine;
            texto += "NombreTabla: " + NombreTabla + Environment.NewLine;
            texto += "idRegistro: " + IdRegistro + Environment.NewLine;
            texto += "idUsuario: " + idUsuario + Environment.NewLine;
            texto += "idLogXml: " + idLogXml + Environment.NewLine;

            texto += Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            EscribeArchivoLog(texto);
        }

        public void RegistraInformacion(string clase, string metodo, string aplicacion, string informacion)
        {
            var texto = Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            texto += "Informacion --> Clase: " + clase + " Metodo: " + metodo + " Hora: " + DateTime.Now + Environment.NewLine + "Nombre de Aplicacion: " + aplicacion + Environment.NewLine;
            texto += "Informe: " + informacion;

            texto += Environment.NewLine + "--------------------------------------------------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine;

            EscribeArchivoLog(texto);
        }
    }
}
