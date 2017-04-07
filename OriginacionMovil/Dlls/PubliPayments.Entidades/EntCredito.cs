using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntCredito
    {
        public DataSet ObtenerCreditoPorOrden(int idOrden)
        {
            var guidTiempos = Tiempos.Iniciar();

            var context = new SistemasCobranzaEntities();
            var cred = (from o in context.Ordenes
                join c in context.Creditos on o.num_Cred equals c.CV_CREDITO
                where o.idOrden == idOrden
                select new
                {
                    o.num_Cred,
                    c.TX_NOMBRE_DESPACHO
                }).First();

            Tiempos.Terminar(guidTiempos);

            return ObtenerCredito(cred.num_Cred, cred.TX_NOMBRE_DESPACHO);
        }

        public string ObtenerCreditoPorNss(string nss, string ordenQueGenera)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sql = "SELECT CV_Credito FROM Creditos WHERE CV_NSS = '" + nss + "' and TX_SOLUCIONES='"+ordenQueGenera+"'";
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            
            return ds.Tables[0].Rows.Count>0?ds.Tables[0].Rows[0][0].ToString():null;
        }

        public DataSet ObtenerCredito(string credito, string despacho)
        {
            var guidTiempos = Tiempos.Iniciar();

            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sql = "SELECT * FROM Creditos WHERE CV_Credito = '" + credito + "'  AND TX_NOMBRE_DESPACHO = '" +
                      despacho + "'";
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);

            Tiempos.Terminar(guidTiempos);
            
            return ds;
        }
        
        /// <summary>
        /// Inserta el credito, genera la orden y la asocia para poder generarles las respuestas cuando es originado en movíl
        /// </summary>
        /// <returns>Regresa un booleano si se pudo realizar o no la operación</returns>
        public bool InsertaCreditoOrden(string credito, string usuario, int idUsuarioPadre,
            int idUsuarioAlta, int idDominio, string ruta, string canal, string etiqueta, string cvContrato, string cvNss,string ordenQueGenera)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "InsertaCreditoOrden",
                        string.Format("InsertaCreditoOrden StoredParams = {0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", credito, usuario, idUsuarioPadre, idUsuarioAlta, idDominio, ruta, canal, etiqueta, cvContrato, ordenQueGenera));

                var guidTiempos = Tiempos.Iniciar();

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("InsertaCreditoOrden", cnn);
                var parametros = new SqlParameter[11];

                parametros[0] = new SqlParameter("@credito", SqlDbType.NVarChar, 50) { Value = credito };
                parametros[1] = new SqlParameter("@usuario", SqlDbType.VarChar, 150) { Value = usuario };
                parametros[2] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) { Value = idUsuarioPadre };
                parametros[3] = new SqlParameter("@idUsuarioAlta ", SqlDbType.Int) { Value = idUsuarioAlta };
                parametros[4] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                parametros[5] = new SqlParameter("@ruta", SqlDbType.VarChar, 10) { Value = ruta };
                parametros[6] = new SqlParameter("@canal", SqlDbType.VarChar, 1) { Value = canal };
                parametros[7] = new SqlParameter("@etiqueta", SqlDbType.VarChar) { Value = etiqueta };
                parametros[8] = new SqlParameter("@CV_CONTRATO", SqlDbType.VarChar) { Value = cvContrato };
                parametros[9] = new SqlParameter("@CV_NSS", SqlDbType.VarChar) { Value = cvNss };
                parametros[10] = new SqlParameter("@TX_Soluciones", SqlDbType.VarChar) { Value = ordenQueGenera }; 

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                bool ok = ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1;

                Tiempos.Terminar(guidTiempos);

                return ok;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "InsertaCreditoOrden",
                        string.Format("InsertaCreditoOrden Error = {0}", ex.Message));
                return false;
            }
            
        }

        public string ValidarDatosCredito(string clabe, string email, string numeroIdentificacion)
        {
            var result = "";
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "ValidarDatosCredito",
                        string.Format("ValidarDatosCredito StoredParams = {0},{1},{2}", clabe, email, numeroIdentificacion));

                var guidTiempos = Tiempos.Iniciar();

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ValidarDatosCredito", cnn);
                var parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@clabe", SqlDbType.NVarChar, 50) { Value = clabe };
                parametros[1] = new SqlParameter("@email", SqlDbType.VarChar, 100) { Value = email };
                parametros[2] = new SqlParameter("@numeroIdentificacion", SqlDbType.VarChar,50) { Value = numeroIdentificacion };
                
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                result=ds.Tables[0].Rows[0][0].ToString();

                Tiempos.Terminar(guidTiempos);

                
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidarDatosCredito",
                        string.Format("ValidarDatosCredito Error = {0}", ex.Message));
                result="Ocurrio un error al registrar el credito";
            }


            return result;
        }

        public static string ValidacionCuenta(string tarjetaEncriptada)
        {
            var result = "";
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "ValidacionCuenta",
                        string.Format("ValidacionCuenta StoredParams = {0}", tarjetaEncriptada));

                var guidTiempos = Tiempos.Iniciar();

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ValidacionCuenta", cnn);
                var parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("@tarjetaEncriptada", SqlDbType.NVarChar, 50) { Value = tarjetaEncriptada };
                

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                result = ds.Tables[0].Rows[0][0].ToString();

                Tiempos.Terminar(guidTiempos);


            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidacionCuenta",
                        string.Format("ValidacionCuenta Error = {0}", ex.Message));
                result = "Ocurrio un error al registrar la cuenta";
            }


            return result;
        }

        public static string CancelarCredito(string nss)
        {
            var result = "";
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "CancelarCredito",
                        string.Format("CancelarCredito StoredParams = {0}", nss));

                var guidTiempos = Tiempos.Iniciar();

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("CancelarCredito", cnn);
                var parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter("@CV_NSS", SqlDbType.NVarChar, 50) { Value = nss };


                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                result = "OK";

                Tiempos.Terminar(guidTiempos);


            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "CancelarCredito",
                        string.Format("CancelarCredito Error = {0}", ex.Message));
                result = "Ocurrio un error al registrar consulta.";
            }


            return result;
        }
    }
}
