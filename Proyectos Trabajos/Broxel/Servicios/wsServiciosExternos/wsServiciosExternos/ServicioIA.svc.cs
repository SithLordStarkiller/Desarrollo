using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Entidades;

namespace wsServiciosExternos
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServicioIA : IServicio
    {
        private const int ServExt = 1;

        public string Echo()
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
                
                const string sql = "select 1 from Usuario";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        public List<UsuarioRelacion> ObtenerRelacionUsuarios(TipoConsulta tipoConsulta, string usuario)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ObtenerRelacionUsuarios " +
                             "@tipoConsulta = " + Convert.ToInt32(tipoConsulta) + "," +
                             "@usuario = N'" + usuario + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return (from DataRow row in ds.Tables[0].Rows
                    select new UsuarioRelacion
                    {
                        Administrador = row["Administrador"].ToString(),
                        Despacho = row["Despacho"].ToString(),
                        Gestor = row["Gestor"].ToString(),
                        Supervisor = row["Supervisor"].ToString(),
                        UsuarioGestor = row["UsuarioGestor"].ToString()
                    }).ToList();
            }
            catch (Exception ex)
            {
                return new List<UsuarioRelacion>
                {
                    new UsuarioRelacion
                    {
                        UsuarioGestor = "ERROR",
                        Administrador = "",
                        Despacho = "",
                        Gestor = "",
                        Supervisor = ""
                    }
                };
            }
        }

        public List<int> OrdenesAutorizadas()
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ObtenerAutorizadasServExternos " +
                                    "@idServExterno = " + ServExt;
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return (from DataRow row in ds.Tables[0].Rows
                        select Convert.ToInt32(row["idOrden"].ToString())
                        ).ToList();
            }
            catch (Exception ex)
            {
                return new List<int>{
                    -1
                };
            }
        }

        //91,155,213

        public List<ValorRespuesta> Respuestas()
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ObtenerRespuestasServExternos " +
                                    "@idServExterno = " + ServExt;
                
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return (from DataRow row in ds.Tables[0].Rows
                        select new ValorRespuesta
                        {
                            IdOrden = Convert.ToInt32(row["idOrden"].ToString()),
                            IdCampo = Convert.ToInt32(row["id"].ToString()),
                            Valor = row["Valor"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                return new List<ValorRespuesta> { new ValorRespuesta
                        {
                            IdOrden = -1,
                            IdCampo = -1,
                            Valor = "ERROR"
                        } };
            }
        }

        public Dictionary<int, string> Dictamenes()
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                const string sql = "exec ConsultasServiciosExternos " +
                                    "@tipo = 1";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return ds.Tables[0].Rows.Cast<DataRow>()
                    .ToDictionary(row => Convert.ToInt32(row["idCampo"].ToString()), row => row["Nombre"].ToString());
            }
            catch (Exception ex)
            {
                return new Dictionary<int, string> { { -1, "ERROR" } };
            }
        }
    }
}
