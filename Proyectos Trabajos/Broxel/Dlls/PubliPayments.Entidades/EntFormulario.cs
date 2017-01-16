using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntFormulario
    {
        public List<FormularioModel> ObtenerListaFormularios(string ruta)
        {
            var resultList = new List<FormularioModel>();
            var ds = ObtenerDatosFormulario(ruta);
            if (ds!=null)
            {
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        resultList.Add(new FormularioModel(ds.Tables[0].Rows[i]["idFormulario"].ToString(), ds.Tables[0].Rows[i]["idAplicacion"].ToString(), ds.Tables[0].Rows[i]["Nombre"].ToString(), ds.Tables[0].Rows[i]["Descripcion"].ToString(), ds.Tables[0].Rows[i]["Version"].ToString(), ds.Tables[0].Rows[i]["Estatus"].ToString(), ds.Tables[0].Rows[i]["FechaAlta"].ToString(), ds.Tables[0].Rows[i]["Captura"].ToString(), ds.Tables[0].Rows[i]["Ruta"].ToString()));
                    }   
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFormulario","GuardarFormulario:" + ex.Message );
                    throw;
                }
                 
            }
            return resultList;
        }

        public DataSet ObtenerDatosFormulario(string ruta)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerDatosFormulario", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10);
                parametros[0].Value = ruta;
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception ex) 
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFormulario","ObtenerDatosFormulario: " + ex.Message);
                if (cnn != null)
                {
                    instancia.CierraConexion(cnn);
                }
            }
            return ds;
        }

        public List<SubFormulario> ObtenerSubFormularios(int idFormularioPadre)
        {
            var context = new SistemasCobranzaEntities();
            var f = from formulario in context.SubFormularios
                where formulario.idFormulario == idFormularioPadre
                select formulario;
            return f.ToList();
        }

        public List<CamposXSubFormulario> ObtenerCampos(int idSubFormulario)
        {
            var context = new SistemasCobranzaEntities();
            var c = from camposXFormulario in context.CamposXSubFormularios
                    where camposXFormulario.idSubFormulario == idSubFormulario
                select camposXFormulario;
            return c.OrderBy(x => x.Orden).ToList();
        }

        public List<CatalogoXCampo> ObtenerCatalogosPorCampo(int idCampo)
        {
            var context = new SistemasCobranzaEntities();
            var catalogos = context.CatalogoXCampoes.Where(x => x.idCampoFormulario == idCampo);
            return catalogos.ToList();
        }

        public FormularioModel InsertaFormulario(FormularioModel modelo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[5];
                
                parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar, 50) { Value = modelo.Nombre };
                parametros[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar, 100) { Value = modelo.Descripcion };
                parametros[2] = new SqlParameter("@Version", SqlDbType.VarChar, 10) { Value = modelo.Version };
                parametros[3] = new SqlParameter("@Captura", SqlDbType.Int) { Value = modelo.Captura };
                parametros[4] = new SqlParameter("@Ruta", SqlDbType.VarChar, 10) { Value = modelo.Ruta};
                
                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaFormulario", parametros);
                if (!string.IsNullOrEmpty(result))
                {
                    var id= Convert.ToInt32(result);
                    modelo.IdFormulario = id;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFormulario", "InsertaFormulario: "+ex.Message);
                modelo.Error = ex.Message;
            }
            return modelo;
        }

        public List<FormularioModel> ObtenerFormularioXOrden(int idOrden, int idusuario, int captura)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) {Value = idOrden};
                parametros[1] = new SqlParameter("@idusuario", SqlDbType.Int) {Value = idusuario};
                parametros[2] = new SqlParameter("@Captura", SqlDbType.Int) {Value = captura};
                var result = instancia.EjecutarDataSet("SqlDefault", "ObtenerFormularioXOrden", parametros);
                return (from DataRow row in result.Tables[0].Rows select new FormularioModel(row["idFormulario"].ToString(), row["idAplicacion"].ToString(), row["Nombre"].ToString(), row["Descripcion"].ToString(), row["Version"].ToString(), row["Estatus"].ToString(), row["FechaAlta"].ToString(), row["Captura"].ToString(), row["Ruta"].ToString())).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFormulario", "ObtenerFormularioXOrden : " + ex.Message);
                return null;
            }
        }

        public int ObtenerBloqueoReverso(int idOrden)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ObtenerBloqueoReverso", cnn);
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idOrden", SqlDbType.VarChar, 10);
            parametros[0].Value = idOrden;

            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
    }
}
