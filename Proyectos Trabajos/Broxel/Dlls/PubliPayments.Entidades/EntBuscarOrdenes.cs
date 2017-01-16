using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntBuscarOrdenes
    {
        public DataTable ObtenerOrdenes(string credito, string nss, string rfc, string nombre, string delegacion, string municipio, int idUsuario)
        {
            DataTable tabla = null;
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntBuscarOrdenes",
                    "ObtenerOrdenes");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerOrdenesCapturaWeb", cnn);
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@Credito", SqlDbType.VarChar, 15) { Value = credito };
                parametros[1] = new SqlParameter("@NSS", SqlDbType.VarChar, 15) { Value = nss };
                parametros[2] = new SqlParameter("@RFC", SqlDbType.VarChar, 15) { Value = rfc };
                parametros[3] = new SqlParameter("@Nombre", SqlDbType.NVarChar, 200) { Value = nombre };
                parametros[4] = new SqlParameter("@Municipio", SqlDbType.NVarChar, 200) { Value = municipio };
                parametros[5] = new SqlParameter("@Delegacion", SqlDbType.VarChar, 100) { Value = delegacion };
                parametros[6] = new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = idUsuario };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    tabla = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntBuscarOrdenes",
                    "ObtenerOrdenes - error: " + ex.Message);
            }
            return tabla;
        }

        public DataSet ObtenerSituacionCombo()
        {
            const string sql = "select codigo Value,Estado Description from CatEstatusOrdenes where codigo in (1,2,3,4,5,6) ";

            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public IEnumerable ObtenerNombreCombo()
        {
            const string sql = "select distinct TX_NOMBRE_ACREDITADO nombre from Creditos WITH (NOLOCK) ";

            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds.Tables[0].AsEnumerable().Select(dataRow => new Nombre { Nom = dataRow.Field<string>("nombre") }).ToList();
        }

        public IEnumerable ObtenerMunicipioCombo(string delegacion)
        {
            List<Municipio> tabla = null;
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntBuscarOrdenes",
                    "ObtenerMunicipioCombo");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerMunicipiosXDelegacion", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Delegacion", SqlDbType.NVarChar) { Value = delegacion };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    tabla =
                        ds.Tables[0].AsEnumerable()
                            .Select(dataRow => new Municipio { Nombre = dataRow.Field<string>("municipio") })
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntBuscarOrdenes",
                    "ObtenerMunicipioCombo - error: " + ex.Message);
            }
            return tabla;


            //const string sql = "select distinct tx_municipio municipio from Creditos WITH (NOLOCK) order by tx_municipio";

            //var conexion = ConexionSql.Instance;
            //var cnn = conexion.IniciaConexion();
            //var sc = new SqlCommand(sql, cnn);
            //var sda = new SqlDataAdapter(sc);
            //var ds = new DataSet();
            //sda.Fill(ds);
            //conexion.CierraConexion(cnn);
            //return ds.Tables[0].AsEnumerable().Select(dataRow => new Municipio { Nombre = dataRow.Field<string>("municipio") }).ToList();

        }

        public DataSet ObtenerDespachosCombo()
        {
            const string sql = "select l.nom_corto Value , l.NombreDominio Description  " +
                               "from dominio l " +
                               "where L.idDominio>2 " +
                               "order by l.NombreDominio ";

            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerSituacionOrden(int idOrden)
        {
            DataSet ds = null;
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntBuscarOrdenes",
                    "ObtenerSituacionOrden");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerOrdenXId", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntBuscarOrdenes",
                    "ObtenerSituacionOrden - error: " + ex.Message);
            }
            return ds;
        }


        public IEnumerable ObtenerDelegacionesFiltro()
        {
            const string sql = "select Descripcion from CatDelegaciones WITH (NOLOCK) ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            return ds.Tables[0].AsEnumerable().Select(dato => new Delegacion { nombre = dato.Field<string>("descripcion") }).ToList();
        }
    }

    public class TablaOrdenes
    {
        public string Seleccion { get; set; }
        public string Orden { get; set; }
        public string Credito { get; set; }
        public string Soluciones { get; set; }
        public string Situacion { get; set; }
        public string Despacho { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Cp { get; set; }
        public string Delegacion { get; set; }
        public string Canal { get; set; }
    }

    public class Delegacion
    {
        public string nombre { get; set; }
    }

    public class Municipio
    {
        public string Nombre { get; set; }
    }

    public class Nombre
    {
        public string Nom { get; set; }
    }

    public class Busqueda
    {
        public string Credito { get; set; }
        public string Nss { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string Delegacion { get; set; }
        public string Municipio { get; set; }
        public int IdUsuario { get; set; }
    }
}
