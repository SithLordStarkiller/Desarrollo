using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntCampoXSubFormulario
    {
        public CampoXSubFormularioModel InsertaCampoXSubFormulario(CampoXSubFormularioModel modelo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[10];
                parametros[0] = new SqlParameter("@idSubFormulario", SqlDbType.Int) { Value = modelo.IdSubformulario };
                parametros[1] = new SqlParameter("@idTipoCampo", SqlDbType.Int) { Value = modelo.IdTipoCampo };
                parametros[2] = new SqlParameter("@NombreCampo", SqlDbType.NVarChar, 250) { Value = modelo.Nombre };
                parametros[3] = new SqlParameter("@Texto", SqlDbType.NVarChar, 500) { Value = modelo.Texto };
                parametros[4] = new SqlParameter("@Valor", SqlDbType.NVarChar, 250){Value = modelo.ValorPrecargado,IsNullable = true};
                parametros[5] = new SqlParameter("@ClasesLinea", SqlDbType.NVarChar, 50) { Value = modelo.ClasesLinea };
                parametros[6] = new SqlParameter("@ClasesTexto", SqlDbType.NVarChar, 50) { Value = modelo.ClasesTexto };
                parametros[7] = new SqlParameter("@ClasesValor", SqlDbType.NVarChar, 50) { Value = modelo.ClasesValor, IsNullable = true };
                parametros[8] = new SqlParameter("@Orden", SqlDbType.Int) { Value = modelo.Orden };
                parametros[9] = new SqlParameter("@Validacion", SqlDbType.NVarChar, 200) { Value = modelo.Validacion, IsNullable = true };

                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaCampoXSubFormulario", parametros);
                if (!string.IsNullOrEmpty(result))
                {
                    var id = Convert.ToInt32(result);
                    modelo.IdCampoFormulario = id;
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntCampoXSubFormulario", "InsertaCampoXSubFormulario: " + ex.Message);
                modelo.Error = ex.Message + " " + (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "");
            }
            return modelo;
        }

        public CampoXSubFormularioModel InsertaCamposXml(string ruta, CampoXSubFormularioModel modelo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@Ruta", SqlDbType.VarChar) { Value = ruta };
                parametros[1] = new SqlParameter("@NombreCampo", SqlDbType.NVarChar, 250) { Value = modelo.Nombre };
                parametros[2] = new SqlParameter("@Valor", SqlDbType.NVarChar, 250) { Value = modelo.Texto, IsNullable = true };

                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaCamposXml", parametros);

                if (!string.IsNullOrEmpty(result))
                {
                    var id = Convert.ToInt32(result);
                    modelo.IdCampoFormulario = id;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntCampoXSubFormulario", "InsertaCamposXml: " + ex.Message + 
                    "Parametros: @Ruta --> " + ruta + " @NombreCampo --> " + modelo.Nombre + " @Valor --> " + modelo.Texto);
                modelo.Error = ex.Message + " " + (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "");
            }
            return modelo;
        }

        public List<CampoXSubFormularioModel> ObtenerCampoSubFormulario(int idSubformulario, string nombre)
        {
            var ds = new DataSet();
            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
            var resultList = new List<CampoXSubFormularioModel>();

            try
            {
                var sc = new SqlCommand("ObtenerCampoXSubformNombre", cnn) { CommandType = CommandType.StoredProcedure };
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@idSubformulario", SqlDbType.Int) { Value = idSubformulario };
                parametros[1] = new SqlParameter("@TextoNombreCampo", SqlDbType.VarChar, 250) { Value = nombre };
                parametros[2] = new SqlParameter("@IgnorarTipo", SqlDbType.VarChar, 20) { Value = 1 };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                sql.CierraConexion(cnn);
                if (ds.Tables.Count > 0)
                {
                    var table = ds.Tables[0];
                    resultList.AddRange(from DataRow row in table.Rows
                        select
                            new CampoXSubFormularioModel
                            {
                               IdCampoFormulario = Convert.ToInt32(row["idCampoFormulario"].ToString()),
                               IdSubformulario = Convert.ToInt32(row["idSubFormulario"].ToString()),
                               IdTipoCampo  = Convert.ToInt32(row["idTipoCampo"].ToString()),
                               Orden = Convert.ToInt32(row["Orden"].ToString()),
                               Nombre = row["NombreCampo"].ToString(),
                               Texto = row["Texto"].ToString(),
                               ValorPrecargado = row["Valor"].ToString(),
                               ClasesLinea = row["ClasesLinea"].ToString(),
                               ClasesTexto = row["ClasesTexto"].ToString(),
                               ClasesValor = row["ClasesValor"].ToString(),
                               Validacion = row["Validacion"].ToString()
                            });
                }
            }
            catch (Exception ex)
            {
                if (cnn!=null)
                {
                    sql.CierraConexion(cnn);    
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntCampoXSubFormulario", "ObtenerCampoSubFormulario: " + ex.Message); 
                return null;
            }

            return resultList;

        }
    }
}
