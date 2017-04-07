using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
   public  class EntCampoSubFormulario
    {
        public int IdCampoFormulario { get; set; }
        public int IdSubformulario { get; set; }
        public int IdTipoCampo { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public string ValorPrecargado { get; set; }
        public string ClasesLinea { get; set; }
        public string ClasesTexto { get; set; }
        public string ClasesValor { get; set; }
        public string Validacion { get; set; }
        public string Error { get; set; }

        public List<EntCatalogoXCampo> ListaCatalogoXCampo = new List<EntCatalogoXCampo>(); 

        public string GuardarCampoSubFormulario(int idSubFormulario)
        {
            var guid = Guid.NewGuid().ToString();

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, GetType().Name,
                "Tiempos - " + guid + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " inicio "); 

            var context = new SistemasCobranzaEntities();
            string error = "";
            try
            {

                var camposFormularioNew = new CamposXSubFormulario
                {
                    idSubFormulario = idSubFormulario,
                    idTipoCampo = IdTipoCampo,
                    NombreCampo = Nombre,
                    Texto = Texto,
                    Valor = ValorPrecargado,
                    ClasesLinea = ClasesLinea,
                    ClasesTexto = ClasesTexto,
                    ClasesValor = ClasesValor,
                    Orden = Orden,
                    Validacion = Validacion
                };
                context.CamposXSubFormularios.Add(camposFormularioNew);
                context.SaveChanges();
                IdCampoFormulario = camposFormularioNew.idCampoFormulario;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntCampoSubFormulario", string.Concat("GuardarCampoSubFormulario: " + 0 + " ", ex.Message, (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
                Error = ex.Message + " " + (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "");
            }

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, GetType().Name,
                 "Tiempos - " + guid + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " final "); 

            return error;
        }

        public List<EntCampoSubFormulario> ObtenerCampoSubFormulario(int idSubformulario, string nombre)
        {
            var guid = Guid.NewGuid().ToString();

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, GetType().Name,
                "Tiempos - " + guid + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " inicio "); 


            var ds = new DataSet();
            var sql = ConexionSql.Instance;
            var cnn = sql.IniciaConexion();
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

            var resultList = new List<EntCampoSubFormulario>();
            if (ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                //fill entity EntCampoSubFormulario
                foreach (DataRow row in table.Rows)
                {
                    var result = new EntCampoSubFormulario();
                    result.IdCampoFormulario = Convert.ToInt32(row["idCampoFormulario"].ToString());
                    result.IdSubformulario = Convert.ToInt32(row["idSubFormulario"].ToString());
                    result.IdTipoCampo = Convert.ToInt32(row["idTipoCampo"].ToString());
                    result.Nombre = row["NombreCampo"].ToString();
                    result.Texto = row["Texto"].ToString();
                    result.ValorPrecargado = row["Valor"].ToString();
                    result.ClasesLinea = row["ClasesLinea"].ToString();
                    result.ClasesTexto = row["ClasesTexto"].ToString();
                    result.ClasesValor = row["ClasesValor"].ToString();
                    result.Orden = Convert.ToInt32(row["Orden"].ToString());
                    resultList.Add(result);
                }
            }// no found data, return null entity

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, GetType().Name,
                    "Tiempos - " + guid + " - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " final "); 

            return resultList;

        }
    }
}
