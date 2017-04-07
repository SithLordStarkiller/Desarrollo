using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class ModelServiciosExternos
    {
        public static string Echo()
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                const string sql = "select 1 from CatProveedores";
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

        public static List<UsuarioRelacion> ObtenerRelacionUsuarios(int tipoConsulta, string usuario,string clave)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ObtenerRelacionUsuarios " +
                             "@tipoConsulta = " + tipoConsulta + "," +
                             "@usuario = N'" + usuario + "',"+
                             "@clave = N'" + clave + "'";
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

        public static List<int> OrdenesAutorizadas(string clave)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ObtenerAutorizadasServExternos " +
                                    "@clave = N'" + clave + "'";
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

        public static List<ValorRespuesta> Respuestas(string clave)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "set dateformat mdy;exec ObtenerRespuestasServExternos " +
                                    "@clave = N'" + clave + "'";

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                sc.CommandTimeout = 120;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return (from DataRow row in ds.Tables[0].Rows
                        select new ValorRespuesta
                        {
                            IdOrden = Convert.ToInt32(row["idOrden"].ToString()),
                            IdCampo = Convert.ToInt32(row["id"].ToString()),
                            Valor = row["Valor"].ToString(),
                            FechaRecepcion = row["FechaRecepcion"].ToString(),
                            NombreCampo = row["NombreCampo"].ToString()
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

        public static List<String> RespuestasMin(string clave)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "set dateformat mdy;exec ObtenerRespuestasServExternos " +
                                    "@clave = N'" + clave + "'";

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                sc.CommandTimeout = 120;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var valores=new List<string>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var str= new ValorRespuesta
                    {
                        IdOrden = Convert.ToInt32(row["idOrden"].ToString()),
                        IdCampo = Convert.ToInt32(row["id"].ToString()),
                        Valor = row["Valor"].ToString(),
                        FechaRecepcion = row["FechaRecepcion"].ToString(),
                        NombreCampo = row["NombreCampo"].ToString()
                    };

                    valores.Add(str.ToString());
                }
                
                return valores;
            }
            catch (Exception ex)
            {
                return new List<String> { "Error|-1|-1|-1|-1" };
            }
        }


        public static List<DiccionarioDictamenes> Dictamenes(string clave)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "exec ConsultasServiciosExternos " +
                                    "@tipo = 1,"+
                                    "@clave = N'" + clave + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return (from DataRow row in ds.Tables[0].Rows
                        select new DiccionarioDictamenes
                        {
                            IdCampo = Convert.ToInt32(row["idCampo"].ToString()),
                            Nombre = row["Nombre"].ToString()
                        }).ToList();
            }
            catch (Exception ex)
            {
                return new List<DiccionarioDictamenes>{ new DiccionarioDictamenes{ IdCampo = -1,Nombre = "ERROR" } };
            }
        }

        public static string ConfirmacionRespuestas(string clave, string fecha)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);

                string sql = "set dateformat mdy;exec ActualizaFechaRespuestasServiciosExternos " +
                                    "@fechaRecepcion = N'" + fecha + "'," +
                                    "@clave = N'" + clave + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return ds.Tables[0].Rows[0]["Resultado"].ToString();
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }
    }
}
