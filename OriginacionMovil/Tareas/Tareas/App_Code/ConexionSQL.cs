//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;

//namespace PubliPayments
//{
//    public sealed class ConexionSQL
//    {
//        private static readonly ConexionSQL instance = new ConexionSQL();
//        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString;

//        private ConexionSQL()
//        {

//        }

//        public SqlConnection IniciaConexion()
//        {
//            var cnn = new SqlConnection(_connectionString);
//            cnn.Open();
//            return cnn;


//        }

//        public DataSet ObtenerRespuestas(int tipo, string idOrden, string numCred, string fechaAlta, string nombre, string calle, string colonia, string municipio, string cp, string estado, int reporte)
//        {
//            var cnn = IniciaConexion();

//            var sc = new SqlCommand("ObtenerRespuestas", cnn);

//            var parametros = new SqlParameter[11];

//            parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) { Value = tipo };
//            parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) { Value = idOrden };
//            parametros[2] = new SqlParameter("@num_Cred", SqlDbType.NVarChar, 20) { Value = numCred };
//            parametros[3] = new SqlParameter("@fechaAlta", SqlDbType.NVarChar, 20) { Value = fechaAlta };
//            parametros[4] = new SqlParameter("@nombre", SqlDbType.NVarChar, 20) { Value = nombre };
//            parametros[5] = new SqlParameter("@calle", SqlDbType.NVarChar, 20) { Value = calle };
//            parametros[6] = new SqlParameter("@colonia", SqlDbType.NVarChar, 20) { Value = colonia };
//            parametros[7] = new SqlParameter("@municipio", SqlDbType.NVarChar, 20) { Value = municipio };
//            parametros[8] = new SqlParameter("@cp", SqlDbType.NVarChar, 20) { Value = cp };
//            parametros[9] = new SqlParameter("@estado", SqlDbType.NVarChar, 20) { Value = estado };
//            parametros[10] = new SqlParameter("@reporte", SqlDbType.Int) { Value = reporte };

//            sc.Parameters.AddRange(parametros);

//            sc.CommandType = CommandType.StoredProcedure;

//            SqlDataAdapter sda = new SqlDataAdapter(sc);
//            DataSet ds = new DataSet();
//            sda.Fill(ds);

//            CierraConexion(cnn);

//            return ds;
//        }

//        public DataSet ObtenerRespuestasGestionadas(int tipo, int reporte)
//        {
//            var ds = new DataSet();

//            try
//            {
//                var cnn = IniciaConexion();

//                var sc = new SqlCommand("ObtenerRespuestasGestionadas", cnn);

//                var parametros = new SqlParameter[2];

//                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) { Value = tipo };
//                parametros[1] = new SqlParameter("@reporte", SqlDbType.Int) { Value = reporte };

//                sc.Parameters.AddRange(parametros);

//                sc.CommandType = CommandType.StoredProcedure;

//                var sda = new SqlDataAdapter(sc);

//                sda.Fill(ds);

//                CierraConexion(cnn);
//            }
//            catch (Exception ex)
//            {
//                Trace.WriteLine(ex.Message);
//            }


//            return ds;
//        }

//        public DataSet ObtenerCamposReferencias(int tipo, int reporte)
//        {
//            var ds = new DataSet();

//            try
//            {
//                var cnn = IniciaConexion();

//                var sc = new SqlCommand("ObtenerCamposReferencias", cnn);

//                var parametros = new SqlParameter[2];

//                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) { Value = tipo };
//                parametros[1] = new SqlParameter("@reporte", SqlDbType.Int) { Value = reporte };

//                sc.Parameters.AddRange(parametros);

//                sc.CommandType = CommandType.StoredProcedure;

//                var sda = new SqlDataAdapter(sc);

//                sda.Fill(ds);

//                CierraConexion(cnn);
//            }
//            catch (Exception ex)
//            {
//                Trace.WriteLine(ex.Message);
//            }


//            return ds;
//        }

//        public DataSet ObtenerOrdenXml(int pool, String credito, int orden)
//        {
//            var cnn = IniciaConexion();
//            var sc = new SqlCommand("ObtieneOrdenXML", cnn);
//            var parametros = new SqlParameter[3];

//            parametros[0] = new SqlParameter("@idPool", SqlDbType.Int) { Value = pool };
//            parametros[1] = new SqlParameter("@Credito", SqlDbType.NVarChar, 50) { Value = credito };
//            parametros[2] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = orden };

//            sc.Parameters.AddRange(parametros);

//            sc.CommandType = CommandType.StoredProcedure;

//            var sda = new SqlDataAdapter(sc);
//            var ds = new DataSet();
//            sda.Fill(ds);

//            CierraConexion(cnn);

//            return ds;
//        }

//        public DataSet CamposXml()
//        {
//            var cnn = IniciaConexion();

//            var sc = new SqlCommand("SELECT * FROM dbo.CamposXml2 ORDER BY Orden", cnn);
//            SqlDataAdapter sda = new SqlDataAdapter(sc);
//            DataSet ds = new DataSet();
//            sda.Fill(ds);
//            CierraConexion(cnn);
//            return ds;
//        }
        
//        public DataSet ObtenerCredito(string credito)
//        {
//            var cnn = IniciaConexion();

//            var sc = new SqlCommand("SELECT TOP 1 * FROM dbo.CREDITOS WHERE CV_CREDITO ='" + credito + "' ORDER BY ID_ARCHIVO DESC", cnn);
//            SqlDataAdapter sda = new SqlDataAdapter(sc);
//            DataSet ds = new DataSet();
//            sda.Fill(ds);
//            CierraConexion(cnn);
//            return ds;
//        }

//        public static ConexionSQL Instance
//        {
//            get
//            {
//                return instance;
//            }
//        }

//        public void CierraConexion(SqlConnection cnn)
//        {
//            cnn.Close();
//        }
//    }
//}
