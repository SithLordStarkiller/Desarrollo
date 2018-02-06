namespace ExamenMvcEf.Helpers
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class Logger
    {

        private string UrlLog { get; }

        private string ConectionString { get; }

        private string NombreArchivo { get; set; }

        private string NombreCompletoDelArchivo { get; }

        private string Extencion { get; }

        private string FechaArchivo { get; }

        private string Archivo { get; }

        public Logger(string urlLog, string connectionString)
        {
            ConectionString = connectionString;
            UrlLog = urlLog;

            Archivo = "LogServer";
            FechaArchivo = DateTime.Now.ToShortDateString().Replace("/", "");
            Extencion = ".Log";

            NombreArchivo = Archivo + FechaArchivo + Extencion;
            NombreCompletoDelArchivo = UrlLog + Archivo + FechaArchivo + Extencion;
        }

        public void EscribeLog(TipoLog tipoLog, string proyecto, string clase, string metodo, string mensage, string log, Exception er, string auxiliar)
        {
            GuardaLogBaseDatos(tipoLog, proyecto, clase, metodo, mensage, log, er, auxiliar);

            if (tipoLog == TipoLog.ErrorCritico || tipoLog == TipoLog.Error)
            {

            }
        }

        private void GuardaLogBaseDatos(TipoLog tipoLog, string proyecto, string clase, string metodo, string mensage, string log, Exception er, string auxiliar)
        {
            try
            {
                var cnn = new SqlConnection(ConectionString);
                cnn.Open();

                var sc = new SqlCommand("Usp_LogAlmacenaLog", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var parametros = new SqlParameter[8];

                parametros[0] = new SqlParameter("@tipoLog", (int)tipoLog);
                parametros[1] = new SqlParameter("@proyecto", proyecto);
                parametros[2] = new SqlParameter("@clase", clase);
                parametros[3] = new SqlParameter("@metodo", metodo);
                parametros[4] = new SqlParameter("@mensage", mensage);
                parametros[5] = new SqlParameter("@log", log);
                parametros[6] = new SqlParameter("@excepcion", er.ToString());
                parametros[7] = new SqlParameter("@auxiliar", auxiliar);

                sc.Parameters.AddRange(parametros);

                sc.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al almacenar en base de datos Usp_LogAlmacenaLog\n");
                Console.WriteLine("Mensage: " + e.Message);
                Console.WriteLine("Inner: " + e.InnerException);
            }
        }

        private bool CreaCarpeta()
        {
            try
            {
                Directory.CreateDirectory(UrlLog);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CreaArchivo()
        {
            var fs = new FileStream(NombreCompletoDelArchivo, FileMode.Create);

            using (StreamWriter tw = new StreamWriter(fs))
            {
                var cabezera = "Fecha de creacion: " + DateTime.Now.ToShortDateString() + "\n Logs  \n";
                tw.WriteLine(cabezera);
                tw.Close();
            }
        }

        private bool ExisteArchivoLog()
        {
            var existeCarpeta = Directory.Exists(UrlLog);

            if (!existeCarpeta)
            {
                CreaCarpeta();
            }
            else
            {
                if (File.Exists(NombreCompletoDelArchivo))
                {
                    using (var reader = new StreamReader(NombreCompletoDelArchivo))
                    {
                        var cabezera = reader.ReadLine();

                        if (cabezera != null)
                        {
                            cabezera = cabezera.Replace("Fecha de creacion: ", "");
                            cabezera = cabezera.Replace(" Logs Universidad ", "");

                            var fechaArchivo = Convert.ToDateTime(cabezera);

                            return fechaArchivo == DateTime.Today;
                        }
                    }
                }

                CreaArchivo();
                return true;
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

            string text;

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

        public enum TipoLog
        {
            Informe = 1,
            Preventivo = 2,
            Error = 3,
            ErrorCritico = 4
        }

        public enum Alamacenamiento
        {
            BaseDeDatos = 0,
            ArchivoPlano = 1,
            ArchivoBd = 2,
            Consola = 4
        }
    }
}
