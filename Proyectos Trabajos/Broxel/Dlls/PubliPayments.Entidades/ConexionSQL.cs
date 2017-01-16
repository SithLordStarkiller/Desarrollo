using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using PubliPayments.Utiles;
using System.Globalization;

namespace PubliPayments.Entidades
{
    public sealed class ConexionSql
    {
// ReSharper disable once InconsistentNaming
        private static readonly ConexionSql instance = new ConexionSql();

        private static string _connectionString;
            //= ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString;

        private ConexionSql()
        {
        }

        public static void EstalecerConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection IniciaConexion()
        {
            var cnn = new SqlConnection(_connectionString);
            cnn.Open();
            return cnn;
        }

        public int ObtenerNoInstanciasCredito(String credito)
        {
            DataSet ds;
            SqlConnection cnn = IniciaConexion();
            try
            {
                String sqlQuery =
                    "SELECT num_cred, COUNT(num_cred) AS Total FROM Ordenes WITH (NOLOCK) GROUP BY num_cred";
                SqlCommand sc = new SqlCommand(sqlQuery, cnn);

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            int result = Int32.Parse(ds.Tables[0].Rows[0]["Total"].ToString());
            return result;
        }

        public DataSet ObtenerErrorEspecifico(String idArchivo)
        {
            DataSet ds;
            SqlConnection cnn = IniciaConexion();
            try
            {
                String sqlQuery = "SELECT Error from ArchivosError WITH (NOLOCK) where id_archivo = " + idArchivo + ";";
                SqlCommand sc = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public String ObtenerErrorEspecificoConRango(String idArchivo, long start)
        {
            String result;
            SqlConnection cnn = IniciaConexion();
            try
            {
                String sqlQuery =
                    String.Format(
                        "Select SUBSTRING(Error, {0}, 16384) as Resultado from ArchivosError with (NOLOCK) where id_archivo = {1} ",
                        start, idArchivo);
                SqlCommand sc = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                DataSet ds = new DataSet();

                sda.Fill(ds);
                result = (String) ds.Tables[0].Rows[0]["Resultado"];
            }
            finally
            {
                CierraConexion(cnn);
            }

            return result;
        }

        public DataSet ObtenerArchivosConError(String tipo, bool errorCompleto)
        {
            DataSet ds;
            SqlConnection cnn = IniciaConexion();
            try
            {
                String substringQuery = errorCompleto ? "b.Error " : "b.id_archivo as Error ";
                String sqlQuery = @"SELECT a.id, a.Archivo, a.Tipo, a.Tiempo, a.Registros, a.Fecha, a.Estatus, " +
                                  substringQuery +
                                  @"FROM Archivos a WITH (NOLOCK)
                                LEFT OUTER JOIN ArchivosError b  WITH (NOLOCK) on a.id = b.id_archivo
                                WHERE a.tipo = 'rar' or a.Tipo = '" + tipo + @"' " +
                                  " ORDER BY a.id DESC";

                SqlCommand sc = new SqlCommand(sqlQuery, cnn);

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public String ObtenerOrdenesErrorRango(String idError, int idUsuarioPadre, long start)
        {
            String result;
            SqlConnection cnn = IniciaConexion();
            try
            {
                String sqlQuery =
                    String.Format(
                        "Select SUBSTRING(Error, {0}, 16384) as Resultado from OrdenesError with (NOLOCK) where idError = {1} and idUsuarioPadre={2} ",
                        start, idError, idUsuarioPadre);
                SqlCommand sc = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                DataSet ds = new DataSet();

                sda.Fill(ds);
                result = (String) ds.Tables[0].Rows[0]["Resultado"];
            }
            finally
            {
                CierraConexion(cnn);
            }

            return result;
        }

        public DataSet ObtenerOrdenesConError(int idUsuarioPadre)
        {
            SqlConnection cnn = IniciaConexion();
            String sqlQuery =
                @"SELECT idError,idUsuarioPadre,Accion,Origen,Error,Fecha FROM OrdenesError WITH (NOLOCK) WHERE idUsuarioPadre=" +
                idUsuarioPadre + " order by idError desc";
            SqlCommand sc = new SqlCommand(sqlQuery, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            CierraConexion(cnn);
            return ds;
        }

        public DataSet InsertaOrdenesError(int idUsuarioPadre, string accion, string origen, string error)
        {
            SqlConnection cnn = IniciaConexion();

            SqlCommand sc = new SqlCommand("InsertaOrdenesError", cnn);

            SqlParameter[] parametros = new SqlParameter[4];

            parametros[0] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int);
            parametros[0].Value = idUsuarioPadre;

            parametros[1] = new SqlParameter("@Accion", SqlDbType.NVarChar, 20);
            parametros[1].Value = accion;

            parametros[2] = new SqlParameter("@Origen", SqlDbType.NVarChar, 50);
            parametros[2].Value = origen;

            parametros[3] = new SqlParameter("@Error", SqlDbType.NVarChar);
            parametros[3].Value = error;

            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(sc);
            DataSet ds = new DataSet();
            try
            {
                sda.Fill(ds);
                CierraConexion(cnn);
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioPadre, "SP:InsertaOrdenesError", ex.Message);
                CierraConexion(cnn);
            }
            return ds;
        }

        public int ObtenerNumeroAsignaciones(int idUsuario)
        {
            int result = 10000;
            var cnn = IniciaConexion();
            try
            {
                var sqlQuery =
                    String.Format(
                        "select count(idOrden) as Cuenta from Ordenes WITH (NOLOCK) where idUsuario = '{0}' and Estatus IN (1,11,15);",
                        idUsuario);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    result = Convert.ToInt32(ds.Tables[0].Rows[0]["Cuenta"]);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return result;
        }

        public int ObtenerRol(int idUsuario)
        {
            int result = 4; //Gestor por default
            var cnn = IniciaConexion();
            try
            {
                var sqlQuery = String.Format("select idRol from Usuario WITH (NOLOCK) where idUsuario = '{0}'",
                    idUsuario);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    result = (int) ds.Tables[0].Rows[0]["idRol"];
            }
            finally
            {
                CierraConexion(cnn);
            }

            return result;
        }

        public DataSet ObtenerArchivos()
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                SqlCommand sc = new SqlCommand("ObtenerArchivos", cnn);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ValidaUsuarioEmail(String email)
        {
            DataSet ds;
            var cnn = IniciaConexion();

            try
            {
                SqlCommand sc = new SqlCommand("ValidarUsuarioEmail", cnn);
                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
                parametros[0].Value = email;

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ValidaUsuarioParcial(String dominio, String usuario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                SqlCommand sc = new SqlCommand("ValidarUsuarioParcial", cnn);
                SqlParameter[] parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter("@Dominio", SqlDbType.NVarChar, 50);
                parametros[0].Value = dominio;
                parametros[1] = new SqlParameter("@Usuario", SqlDbType.NVarChar, 50);
                parametros[1].Value = usuario;

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ObtenerRespuestasBitacora(int tipo, string idOrden, int reporte, int idUsuarioPadre,
            string tipoFormulario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerRespuestasBitacora", cnn);
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) {Value = idOrden};
                parametros[2] = new SqlParameter("@reporte", SqlDbType.Int) {Value = reporte};
                parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                parametros[4] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar, 10) {Value = tipoFormulario};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 300;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ResultadoVisita(int tipo, string idOrden, int reporte, int idUsuarioPadre, string tipoFormulario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerRespuestasBitacora2", cnn);
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) {Value = idOrden};
                parametros[2] = new SqlParameter("@reporte", SqlDbType.Int) {Value = reporte};
                parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                parametros[4] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar, 10) {Value = tipoFormulario};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 300;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ObtenerRespuestasUsuario(int tipo, string idOrden, int reporte, int idUsuarioPadre,
            string estatus, string tipoFormulario)
        {
            DataSet ds;
            SqlConnection cnn = IniciaConexion();

            try
            {
                SqlCommand sc = new SqlCommand("ObtenerRespuestasUsuario", cnn);

                SqlParameter[] parametros = new SqlParameter[6];

                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) {Value = idOrden};
                parametros[2] = new SqlParameter("@reporte", SqlDbType.Int) {Value = reporte};
                parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                parametros[4] = new SqlParameter("@Estatus", SqlDbType.VarChar) {Value = estatus};
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) {Value = tipoFormulario};

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ObtenerOrdenes(int tipo, int idUsuarioPadre, string numCred, int idUsuario, string tipoFormulario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerOrdenes", cnn);

                var parametros = new SqlParameter[5];

                parametros[0] = new SqlParameter("@Tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@idUsuarioPadre", SqlDbType.NVarChar, 20) {Value = idUsuarioPadre};
                parametros[2] = new SqlParameter("@num_Cred", SqlDbType.VarChar, 20) {Value = numCred};
                parametros[3] = new SqlParameter("@idUsuario", SqlDbType.Int) {Value = idUsuario};
                parametros[4] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) {Value = tipoFormulario};


                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;

                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ObtenerCreditosNoAsignados(int idUsuarioPadre, int idDominio, string tipoFormulario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerCreditosNoAsignados", cnn);
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@usuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                parametros[1] = new SqlParameter("@idDominio", SqlDbType.Int) {Value = idDominio};
                parametros[2] = new SqlParameter("@tipoFormulario", SqlDbType.VarChar, 10) {Value = tipoFormulario};

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);

            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ObtenerCreditos(string numCred, int usuarioPadre, int idDominio, string tipoFormulario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerCreditos", cnn);
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@num_cred", SqlDbType.VarChar, 20) {Value = numCred};
                parametros[1] = new SqlParameter("@usuarioPadre", SqlDbType.Int) {Value = usuarioPadre};
                parametros[2] = new SqlParameter("@idDominio", SqlDbType.Int) {Value = idDominio};
                parametros[3] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) {Value = tipoFormulario};

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet InsertaDominio(String dominio, String nomCorto, String usuario, String nombre, String password,
            String email)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                SqlCommand sc = new SqlCommand("InsertaDominio", cnn);

                SqlParameter[] parametros = new SqlParameter[6];

                parametros[0] = new SqlParameter("@Dominio", SqlDbType.NVarChar, 50);
                parametros[0].Value = dominio;
                parametros[1] = new SqlParameter("@nom_corto", SqlDbType.NVarChar, 50);
                parametros[1].Value = nomCorto;
                parametros[2] = new SqlParameter("@Usuario", SqlDbType.NVarChar, 50);
                parametros[2].Value = usuario;
                parametros[3] = new SqlParameter("@Nombre", SqlDbType.NVarChar, 30);
                parametros[3].Value = nombre;
                parametros[4] = new SqlParameter("@Password", SqlDbType.NVarChar, 130);
                parametros[4].Value = password;
                parametros[5] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
                parametros[5].Value = email;

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet InsertaLlave(String llave, int idUsuario, DateTime fechaVencimiento)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {

                SqlCommand sc = new SqlCommand("InsertaLlave", cnn);

                SqlParameter[] parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@Llave", SqlDbType.NVarChar, 15);
                parametros[0].Value = llave;
                parametros[1] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                parametros[1].Value = fechaVencimiento;
                parametros[2] = new SqlParameter("@idUsuario", SqlDbType.Int);
                parametros[2].Value = idUsuario;

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);

            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ValidarLlaveRecuperacion(String llave)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {

                SqlCommand sc = new SqlCommand("ValidarLlaveRecuperacion", cnn);

                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("@Llave", SqlDbType.NVarChar, 15);
                parametros[0].Value = llave;

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);


            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet ObtieneOrdenXml(int pool, String credito, int orden)
        {
            var cnn = IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtieneOrdenXML", cnn);
                var parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@idPool", SqlDbType.Int) {Value = pool};
                parametros[1] = new SqlParameter("@Credito", SqlDbType.NVarChar, 50) {Value = credito};
                parametros[2] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = orden};

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);

                sda.Fill(ds);

                CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                CierraConexion(cnn);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "ConexionSQL",
                    "ObtenerOrdenXml" + ex.Message + " Stack:" + ex.StackTrace);
            }

            return ds;
        }

        public DataSet ObtieneOrdenXCreditoXml(String credito)
        {
            var cnn = IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtieneOrdenXCreditoXML", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Credito", SqlDbType.NVarChar, 50) {Value = credito};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);

                sda.Fill(ds);

                CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                CierraConexion(cnn);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "ConexionSQL",
                    "ObtieneOrdenXCreditoXml" + ex.Message + " Stack:" + ex.StackTrace);
            }


            return ds;
        }


        public DataSet ObtenerCredito(string credito)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {

                var sc =
                    new SqlCommand(
                        "SELECT TOP 1 * FROM dbo.CREDITOS WITH (NOLOCK) WHERE CV_CREDITO ='" + credito +
                        "' ORDER BY ID_ARCHIVO DESC", cnn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet CamposXml()
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("SELECT * FROM dbo.CamposXml2 WITH (NOLOCK) ORDER BY Orden", cnn);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ValidarBaja(int idUsuario)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                SqlCommand sc = new SqlCommand("ValidarBaja", cnn);
                SqlParameter[] parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int);
                parametros[0].Value = idUsuario;
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);


            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;
        }

        public DataSet GeneraOrdenXml(string credito, int idUsuario, int idUsuarioPadre,
            int idUsuarioAlta, int idDominio, int idOrden)
        {
            SqlConnection cnn = IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("GeneraOrdenXML3", cnn) {CommandType = CommandType.StoredProcedure};
                var p = new SqlParameter[7];
                p[0] = new SqlParameter("@idPool", SqlDbType.Int) {Value = 1};
                p[1] = new SqlParameter("@Credito", SqlDbType.NVarChar, 50) {Value = credito};
                p[2] = new SqlParameter("@idUsuario", SqlDbType.Int) {Value = idUsuario};
                p[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                p[4] = new SqlParameter("@idUsuarioAlta", SqlDbType.Int) {Value = idUsuarioAlta};
                p[5] = new SqlParameter("@idDominio", SqlDbType.Int) {Value = idDominio};
                p[6] = new SqlParameter("@idOrden", SqlDbType.Int) {Value = idOrden};
                sc.Parameters.AddRange(p);
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "GeneraOrdenXml", ex.Message);
            }
            CierraConexion(cnn);
            return ds;
        }

        public DataSet Source_GetIndicadores(int nUser, int nDominio, int nRol, string sUser, string enumDashBoard,
            string sDelegacion, string sEstado, string sDespacho, string sSupervisor, string sGestor)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("SpU_DashBoard_Handler", cnn);
                var parametros = new SqlParameter[12];
                parametros[0] = new SqlParameter("@Accion", SqlDbType.VarChar, 100) {Value = "Get_Indicadores"};
                parametros[1] = new SqlParameter("@SubAccion", SqlDbType.VarChar, 100) {Value = ""};
                parametros[2] = new SqlParameter("@fi_Usuario", SqlDbType.Int) {Value = nUser};
                parametros[3] = new SqlParameter("@fc_Usuario", SqlDbType.VarChar, 50) {Value = sUser};
                parametros[4] = new SqlParameter("@fi_Dominio", SqlDbType.Int) {Value = nDominio};
                parametros[5] = new SqlParameter("@fi_Rol", SqlDbType.Int) {Value = nRol};
                parametros[6] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar, 100) {Value = enumDashBoard};
                parametros[7] = new SqlParameter("@fc_Delegacion", SqlDbType.VarChar, 100) {Value = sDelegacion};
                parametros[8] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar, 100) {Value = sDespacho};
                parametros[9] = new SqlParameter("@fc_Estado", SqlDbType.VarChar, 100) {Value = sEstado};
                parametros[10] = new SqlParameter("@fc_idUsuarioPadre", SqlDbType.VarChar, 100) {Value = sSupervisor};
                parametros[11] = new SqlParameter("@fc_idUsuario", SqlDbType.VarChar, 100) {Value = sGestor};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ObtenerCamposReferencias(int tipo, int reporte)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerCamposReferencias", cnn);
                sc.CommandTimeout = 0;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@reporte", SqlDbType.Int) {Value = reporte};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ObtenerRespuestasGestionadas(int tipo, int reporte, int tipoArchivo)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerRespuestasGestionadas", cnn);
                sc.CommandTimeout = 0;
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) {Value = tipo};
                parametros[1] = new SqlParameter("@reporte", SqlDbType.Int) {Value = reporte};
                parametros[2] = new SqlParameter("@tipoArchivo", SqlDbType.Int) {Value = tipoArchivo};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public int ActualizarEstatusUsuarioOrdenes(string ordenes, int estatus, int actualizaUsuario,
            bool actualizaFecha, bool actualizaSiEstatusIgual)
        {
            int result;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ActualizarEstatusUsuarioOrdenes", cnn);
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.VarChar, 1500) {Value = ordenes};
                parametros[1] = new SqlParameter("@Estatus", SqlDbType.Int) {Value = estatus};
                parametros[2] = new SqlParameter("@ActualizaUsuario", SqlDbType.Int) {Value = actualizaUsuario};
                parametros[3] = new SqlParameter("@ActualizaFecha", SqlDbType.Bit) {Value = actualizaFecha};
                parametros[4] = new SqlParameter("@ActSiEstatusIgual", SqlDbType.Bit) {Value = actualizaSiEstatusIgual};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                result = sc.ExecuteNonQuery();
            }
            finally
            {
                CierraConexion(cnn);
            }
            return result;
        }

        public int ActualizarAuxiliar(string ordenes, string auxiliar)
        {
            int result;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ActualizarAuxiliar", cnn);
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.VarChar, 1500) {Value = ordenes};
                parametros[1] = new SqlParameter("@TextoAuxiliar", SqlDbType.VarChar, 2250) {Value = auxiliar};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                result = sc.ExecuteNonQuery();
            }
            finally
            {
                CierraConexion(cnn);
            }
            return result;
        }

        public DataSet ObtenerListaEstados(String[] estados)
        {
            var cnn = IniciaConexion();
            var ds = new DataSet();
            try
            {
                String arg = estados.Aggregate((current, next) => current + "," + next);
                var sc = new SqlCommand("ObtenerListaEstados", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@ListaOrdenes", SqlDbType.VarChar, 1500) {Value = arg};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerListaEstados", ex.Message);
            }

            CierraConexion(cnn);
            return ds;
        }

        public int CancelarOrdenes(string ordenes)
        {
            var ds = new DataSet();
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("CancelarOrdenes", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.NVarChar, 1500) {Value = ordenes};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            int result = Int32.Parse(ds.Tables[0].Rows[0]["Exito"].ToString());
            return result;
        }

        /// <summary>
        /// retrna informacion para generar el reporte y actualiza el estatus del mismo
        /// </summary>
        /// <param name="idUsuarioPadre">id de usuario supervisor del cual se va a generar el reporte</param>
        /// <param name="idUsuarioReporte">id del usuario propietario del reporte, valor por default 0 y tomara el idUsuarioPadre como propietario</param>
        /// <returns></returns>
        public DataSet ObtenerReporteGeneral(int idUsuarioPadre, int idUsuarioReporte)
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {


                var sc = new SqlCommand("ObtenerReporteGeneral", cnn);

                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                parametros[1] = new SqlParameter("@idUsuarioReporte", SqlDbType.Int) {Value = idUsuarioReporte};

                sc.Parameters.AddRange(parametros);

                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 300;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);

            }
            finally
            {
                CierraConexion(cnn);
            }

            return ds;

        }

        //public DataSet InsertaReporteGeneral(int idReporte, StringBuilder reporteTxt, int idPadre)
        //{
        //    DataSet ds;
        //    var cnn = IniciaConexion();
        //    try
        //    {
        //        var sc = new SqlCommand("InsertaReporte", cnn);

        //        var parametros = new SqlParameter[3];
        //        parametros[0] = new SqlParameter("@idReporte", SqlDbType.Int) {Value = idReporte};
        //        parametros[1] = new SqlParameter("@ReporteTxt", SqlDbType.NVarChar) {Value = reporteTxt.ToString()};
        //        parametros[2] = new SqlParameter("@idPadre", SqlDbType.Int) {Value = idPadre};
        //        sc.Parameters.AddRange(parametros);

        //        sc.CommandType = CommandType.StoredProcedure;

        //        var sda = new SqlDataAdapter(sc);
        //        ds = new DataSet();
        //        sda.Fill(ds);
        //    }
        //    finally
        //    {
        //        CierraConexion(cnn);
        //    }


        //    return ds;

        //}

        public DataSet ObtenerEstatusReporte(int idUsuarioPadre, int? idReporte, int tipo)
        {
            var cnn = IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sqlQuery = idReporte != null
                    ? String.Format(
                        "select idReporte,tipo,Estatus,Fecha from Reportes WITH (NOLOCK) where idReporte = '{0}';",
                        idReporte)
                    : String.Format(
                        "select top 1 idReporte,tipo,Estatus,Fecha from Reportes WITH (NOLOCK) where idPadre = '{0}' and Tipo = '{1}' AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) order by Fecha desc;",
                        idUsuarioPadre, tipo);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                CierraConexion(cnn);

            }
            catch (Exception)
            {
                CierraConexion(cnn);
                return ds;

            }

            return ds;
        }

        public String DescargarReporteParticionado(String idReporte, long start)
        {
            string result;
            var cnn = IniciaConexion();
            try
            {
                String sqlQuery =
                    String.Format(
                        "Select SUBSTRING(ReporteTxt, {0}, 32768) as Reporte from Reportes WITH (NOLOCK) where idReporte = {1} ",
                        start, idReporte);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();

                sda.Fill(ds);
                result = (String) ds.Tables[0].Rows[0]["Reporte"];
            }
            finally
            {
                CierraConexion(cnn);
            }
            return result;
        }

        public string DescargarReporteParticionado(string idPadre, long start, string fecha)
        {
            string result;
            var cnn = IniciaConexion();
            try
            {
                String sqlQuery =
                    String.Format(
                        "Select SUBSTRING(ReporteTxt, {0}, 32768) as Reporte from Reportes WITH (NOLOCK) where idPadre = {1} AND CONVERT(CHAR(6), Fecha, 112) = {2}",
                        start, idPadre, fecha);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();

                sda.Fill(ds);
                result = (String) ds.Tables[0].Rows[0]["Reporte"];
            }
            finally
            {
                CierraConexion(cnn);
            }
            return result;
        }

        public DataTable ObtenerHistoricoCredito(string credito, int idDominio, string[] log)
        {
            if (log[0] != "-1")
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, Convert.ToInt32(log[0]), log[1],
                    "ObtenerHistorico CR " + credito);
            }
            string fechaTope = DateTime.Now.AddDays(-DateTime.Now.Day)
                .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sqlQuery =
                    String.Format(
                        "SELECT TOP 10 num_cred,Estatus,Dictamen,idDominio,Fecha,PromesaPago,FechaPromesa,Saldo FROM dbo.HistoricoOrdenes WITH (NOLOCK) where num_cred='{0}'and ( Fecha<convert (datetime,'{1}',121) or idDominio={2}) order by fecha DESC;",
                        credito, fechaTope, idDominio);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds.Tables[0];
        }

        public DataTable ObtenerHistoricoOrden(string idOrden, int idDominio, string[] log)
        {
            if (log[0] != "-1")
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, Convert.ToInt32(log[0]), log[1],
                    "ObtenerHistorico OR " + idOrden);
            }
            string fechaTope = DateTime.Now.AddDays(-DateTime.Now.Day)
                .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sqlQuery =
                    String.Format(
                        "SELECT TOP 10 num_cred,Estatus,Dictamen,idDominio,Fecha,PromesaPago,FechaPromesa,Saldo FROM dbo.HistoricoOrdenes WITH (NOLOCK) where num_cred in (select top 1 num_cred from Ordenes WITH (NOLOCK) where idOrden={0}) and ( Fecha<convert (datetime,'{1}',121) or idDominio={2}) order by fecha DESC;",
                        idOrden, fechaTope, idDominio);
                var sc = new SqlCommand(sqlQuery, cnn);
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds.Tables[0];
        }

        public DataSet ObtenerReporteUnificado()
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerReporteUnificado", cnn)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet ObtenerReporteRegistroAsesores()
        {
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerReporteRegistroAsesores", cnn)
                {
                    CommandTimeout = 0,
                    CommandType = CommandType.StoredProcedure
                };
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }
            return ds;
        }

        public DataSet InsertaHistoricoOrdenes(bool general)
        {
            SqlConnection cnn = IniciaConexion();

            try
            {
                var sc = new SqlCommand("InsertaHistoricoOrdenes", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@General", SqlDbType.Bit) {Value = general};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 300;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                CierraConexion(cnn);
                return ds;
            }
            catch (Exception ex)
            {
                if (cnn != null)
                {
                    CierraConexion(cnn);
                }
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "ConexionSQL",
                    "InsertaHistoricoOrdenes" + ex.Message + " Stack:" + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los creditos que han sufrido una reasignacion que no es permitida
        /// </summary>
        /// <returns></returns>
        public DataSet ObtenerCreditosAMover()
        {

            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerCreditosAMover", cnn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 7200
                };
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);

            }
            return ds;
        }


        /// <summary>
        /// Obtiene las ordenes-Creditos que han sufrido una reasignacion 
        /// </summary>
        /// <returns></returns>
        public DataSet ObtenerOrdenesAMover()
        {

            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("ObtenerOrdenesAMover", cnn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 7200
                };
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);

            }
            return ds;
        }

        /// <summary>
        /// Regresa los creditos a los despachos que has sufrido de una reasignacion de creditos pero estos ya tienen como gestion un convenio
        /// </summary>
        /// <param name="creditos">Creditos a mover separados por comas</param>
        public void CreditosAMover(string creditos)
        {
  
            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("CreditosAMover", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Creditos", SqlDbType.NVarChar, 2000) {Value = creditos};

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 7200;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);

            }

        }


        /// <summary>
        /// Cancela Ordenes pertenecientes a un credito que ha sido reasignado
        /// </summary>
        /// <param name="ordenes">Ordenes a mover</param>
        public void OrdenesAMover(string ordenes)
        {

            DataSet ds;
            var cnn = IniciaConexion();
            try
            {
                var sc = new SqlCommand("OrdenesAMover", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Ordenes", SqlDbType.VarChar, 1500) { Value = ordenes };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.CommandTimeout = 7200;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
            }
            finally
            {
                CierraConexion(cnn);
            }

        }


        public static ConexionSql Instance
        {
            get { return instance; }
        }

        public void CierraConexion(SqlConnection cnn)
        {
            if (cnn == null) return;
            cnn.Close();
            cnn.Dispose();
        }
    }
}