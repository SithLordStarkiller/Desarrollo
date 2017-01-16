using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntGestionadas
    {

        public DataSet ObtenerRespuestasOriginacion(int idUsuarioPadre, string estatus, string tipoFormulario)
        {
            var sql = "exec ObtenerRespuestasOriginacion " +
                      "@idUsuarioPadre = " +idUsuarioPadre + ", " +
                      "@Estatus = N'" + estatus + "', " +
                      "@TipoFormulario = N'" + tipoFormulario + "' ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerRespuestasMYO(int idUsuario, string estatus, string tipoFormulario)
        {
            var sql = "exec ObtenerRespuestasMYO " +
                      "@idUsuario = " + idUsuario + ", " +
                      "@Estatus = N'" + estatus + "', " +
                      "@TipoFormulario = N'" + tipoFormulario + "' ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerDocumentosOrden(string idOrden,string origen)
        {
            var sql = "exec " + (origen.Contains("MYO") ? "ObtenerDocumentosMYO " : "ObtenerDocumentosOrden ") +
                      "@idOrden = " +idOrden;
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public void BorrarImagen(int idOrden,string fotosABorrar)
        {
            var sql = "exec BorrarImagenOrden " +
                      "@idOrden = " + idOrden +", "+
                      "@fotosABorrar = N'" + fotosABorrar+"'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            
        }

        public void BorrarDocumento(int idOrden, string campo)
        {
            var sql = "exec BorrarDocumentoOrden " +
                      "@idOrden = " + idOrden + ", " +
                      "@campo = N'" + campo + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

        }

        public void AgregarDocumentoOrden(int idOrden ,String campo ,String fullpath )
        {
            var sql = "exec AgregarDocumentoOrden " +
                      "@idOrden = " + idOrden + ", " +
                      "@campo = N'" + campo + "', " +
                      "@valor = N'" + fullpath + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

        public void ActualizaRutaImagenes(string idOrden,string idCampo,string ruta)
        {
            var sql = "exec ActualizaRutaImagenes " +
                      "@idOrden = " + idOrden + ", " +
                      "@idCampo = " + idCampo + ", " +
                      "@ruta = N'" + ruta + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

        public string ObtenerCifradoTc(string idOrden)
        {
            var sql = "exec ObtenerCifradoTc " +
                      "@idOrden = " + idOrden;
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return ds.Tables[0].Rows[0][0].ToString();
        }
        public DataSet ObtenerRespuestasSira(int idUsuarioPadre, string estatus, string tipoFormulario)
        {
            var sql = "exec ObtenerRespuestaSira " +
                      "@idUsuarioPadre = " + idUsuarioPadre + ", " +
                      "@Estatus = N'" + estatus + "', " +
                      "@TipoFormulario = N'" + tipoFormulario + "' ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public string AutorizarAnalisis(string textoOrdenes)
        {
            string result = "0";
            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.VarChar, 1500) { Value = textoOrdenes };
              
                result = instancia.EjecutarEscalar("SqlDefault", "AutorizarAnalisis", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntGestionadas", "AutorizarAnalisis: " + ex.Message);
                
            }
            return result;
        }
    }
}
