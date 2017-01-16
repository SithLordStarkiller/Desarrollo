using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace PubliPayments.Entidades
{
    public class IndicadoresDashBoard
    {
        readonly int _nUser = -1;
        private readonly int _nRol = -1;
        private readonly int _nDominio = -1;
       
        private readonly String _sUser = "";
        
        private readonly FiltrosDashboard _filtro;
        private readonly String _tipoDashBoard;
        private readonly String _tipoFormulario;

        public IndicadoresDashBoard(FiltrosDashboard filtrosDash,String sUser,String tipoDashboard,int nUser,int nRol,int nDominio, string tipoFormulario)
        {
            _filtro = filtrosDash;
            _nUser = nUser;
            _nRol = nRol;
            _nDominio = nDominio;
            _sUser = sUser;
            _tipoDashBoard = tipoDashboard;
            _tipoFormulario=tipoFormulario;
        }
                

        public List<IndicadorDashboard> ClaveIndicadores()
        {
            var context = new SistemasCobranzaEntities();

            var listaIndicador = from e in context.ObtenerClavesDashboard( _tipoDashBoard)
                                 where e.fi_Parte != 0
                                 select e;

            var lista = listaIndicador.Select(hijo => new IndicadorDashboard { FcClave = hijo.fc_Clave, FiParte = hijo.fi_Parte }).ToList();

            return lista;
        }

        public List<IndicadorDashboard> TablaIndicadores(String indicador)
        {
            var context = new SistemasCobranzaEntities();
            

            var listaIndicador = from e in context.ObtenerIndicadorDashboard(null,null,indicador,_nUser, _sUser, _nDominio, _nRol, _tipoDashBoard, _filtro.Delegacion, _filtro.Despacho,
                                     _filtro.Estado, _filtro.Supervisor, _filtro.Gestor,null)                                 
                                 select e;

            var lista = listaIndicador.Select(hijo => new IndicadorDashboard {FiParte = hijo.fi_Parte ,FcDescripcion = hijo.descripcion,FcClave = hijo.fc_Clave, FiPorcentaje = hijo.porcentaje.ToString(), FiValue = Convert.ToInt32(hijo.value) }).ToList();
            
            

            return lista;
        }

        public List<IndicadorDashboard> TablaBloqueIndicadores1(int bloque)
        {
            try
            {
                var sql = "EXEC [dbo].[ObtenerIndicadorBloqueDashboard] " +
                          "@Accion = NULL, " +
                          "@SubAccion = NULL, " +
                          "@Bloque = "+bloque+", " +
                          "@fi_Usuario = "+_nUser+", " +
                          "@fc_Usuario = N'"+_sUser+"', " +
                          "@fi_Dominio = "+_nDominio+", " +
                          "@fi_Rol = "+_nRol+", " +
                          "@fc_DashBoard = N'"+_tipoDashBoard+"', " +
                          "@fc_Delegacion = N'"+_filtro.Delegacion+"', " +
                          "@fc_Despacho = N'"+_filtro.Despacho+"', " +
                          "@fc_Estado = N'"+_filtro.Estado+"', " +
                          "@fc_idUsuarioPadre = N'"+_filtro.Supervisor+"', " +
                          "@fc_idUsuario = N'"+_filtro.Gestor+"', " +
                          "@debug = N'null', " +
                          "@TipoFormulario = N'"+_tipoFormulario+"',"+
                          "@esCallCenter = N'" + ConfigurationManager.AppSettings["EsCallCenter"] + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var lista=(from DataRow row in ds.Tables[0].Rows
                    select new IndicadorDashboard
                    {
                        FiParte = Convert.ToInt32(row["fi_Parte"].ToString()), 
                        FcDescripcion = row["descripcion"].ToString(), 
                        FcClave = row["fc_Clave"].ToString(), 
                        FiPorcentaje = row["porcentaje"].ToString(), 
                        FiValue = Convert.ToInt32(row["value"])
                    }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<IndicadorDashboard> TablaBloqueIndicadores2(int bloque)
        {
            try
            {
                var sql = "EXEC [dbo].[ObtenerIndicadorBloqueDashboard] " +
                          "@Accion = NULL, " +
                          "@SubAccion = NULL, " +
                          "@Bloque = " + bloque + ", " +
                          "@fi_Usuario = " + _nUser + ", " +
                          "@fc_Usuario = N'" + _sUser + "', " +
                          "@fi_Dominio = " + _nDominio + ", " +
                          "@fi_Rol = " + _nRol + ", " +
                          "@fc_DashBoard = N'" + _tipoDashBoard + "', " +
                          "@fc_Delegacion = N'" + _filtro.Delegacion + "', " +
                          "@fc_Despacho = N'" + _filtro.Despacho + "', " +
                          "@fc_Estado = N'" + _filtro.Estado + "', " +
                          "@fc_idUsuarioPadre = N'" + _filtro.Supervisor + "', " +
                          "@fc_idUsuario = N'" + _filtro.Gestor + "', " +
                          "@debug = N'null', " +
                          "@TipoFormulario = N'" + _tipoFormulario + "'," +
                          "@esCallCenter = N'" + ConfigurationManager.AppSettings["EsCallCenter"] + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var lista = (from DataRow row in ds.Tables[0].Rows
                             select new IndicadorDashboard
                             {
                                 FiParte = Convert.ToInt32(row["fi_Parte"].ToString()),
                                 FcDescripcion = row["descripcion"].ToString(),
                                 FcClave = row["fc_Clave"].ToString(),
                                 FiPorcentaje = Convert.ToInt32(row["porcentaje"]).ToString(),
                                 FiValue = Convert.ToInt32(row["value"])
                             }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<IndicadorDashboard> TablaBloqueIndicadores3(int bloque)
        {
            try
            {
                var sql = "EXEC [dbo].[ObtenerIndicadorBloqueDashboard] " +
                          "@Accion = NULL, " +
                          "@SubAccion = NULL, " +
                          "@Bloque = " + bloque + ", " +
                          "@fi_Usuario = " + _nUser + ", " +
                          "@fc_Usuario = N'" + _sUser + "', " +
                          "@fi_Dominio = " + _nDominio + ", " +
                          "@fi_Rol = " + _nRol + ", " +
                          "@fc_DashBoard = N'" + _tipoDashBoard + "', " +
                          "@fc_Delegacion = N'" + _filtro.Delegacion + "', " +
                          "@fc_Despacho = N'" + _filtro.Despacho + "', " +
                          "@fc_Estado = N'" + _filtro.Estado + "', " +
                          "@fc_idUsuarioPadre = N'" + _filtro.Supervisor + "', " +
                          "@fc_idUsuario = N'" + _filtro.Gestor + "', " +
                          "@debug = N'null', " +
                          "@TipoFormulario = N'" + _tipoFormulario + "'," +
                          "@esCallCenter = N'" + ConfigurationManager.AppSettings["EsCallCenter"] + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var lista = (from DataRow row in ds.Tables[0].Rows
                             select new IndicadorDashboard
                             {
                                 FiParte = Convert.ToInt32(row["fi_Parte"].ToString()),
                                 FcDescripcion = row["descripcion"].ToString(),
                                 FcClave = row["fc_Clave"].ToString(),
                                 FiPorcentaje = Convert.ToInt32(row["porcentaje"]).ToString(),
                                 FiValue = Convert.ToInt32(row["value"])
                             }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





    }
}