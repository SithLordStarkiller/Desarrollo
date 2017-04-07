using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntFuncionesJs
    {
        public FuncionXCampoModel InsertaFuncionesJs(FuncionXCampoModel modelo)
        {
            if (modelo.FuncionFinal.Length == 0)
                return modelo;
            try
            {
            var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[5];

                parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar,50) { Value = null };
                parametros[1] = new SqlParameter("@Validacion", SqlDbType.VarChar,8000) { Value = null };
                parametros[2] = new SqlParameter("@FuncionSI", SqlDbType.VarChar,8000) { Value = modelo.FuncionFinal.ToString() };
                parametros[3] = new SqlParameter("@FuncionNo", SqlDbType.VarChar,8000) { Value = null };
                parametros[4] = new SqlParameter("@idFormulario", SqlDbType.Int) { Value = modelo.IdFormulario };


                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaFuncionesJs", parametros);
                if (!string.IsNullOrEmpty(result))
                {
                    var id = Convert.ToInt32(result);
                    modelo.IdFuncionJs = id;  
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFuncionesJs", "InsertaFuncionesJs : " + ex.Message);
                modelo.Error = ex.Message;
            }
            return modelo;
        }

    
        public void InsFuncionesXCampos(int idFuncionJs, string textoidCampoFormulario)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@idFuncionJS", SqlDbType.Int) { Value = idFuncionJs };
                parametros[1] = new SqlParameter("@TextoidCampoFormulario", SqlDbType.VarChar, 250) { Value = textoidCampoFormulario };
                instancia.EjecutarDataSet("SqlDefault", "InsFuncionesXCampos", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFuncionesJs", "InsFuncionesXCampos : " + ex.Message);
            }
            
        }

        public DataSet ObtenerFuncionesJs(int idformulario)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idformulario", SqlDbType.Int) { Value = idformulario };
                return instancia.EjecutarDataSet("SqlDefault", "ObtenerFuncionesJs", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFuncionesJs", "ObtenerFuncionesJs : " + ex.Message);
                return null;
            }
        }

        public DataSet ObtenerFuncionesCampo(int usuarioLog, string campoPadre, int idFormulario)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@campoPadre", SqlDbType.VarChar, 50) { Value = campoPadre };
                parametros[1] = new SqlParameter("@idFormulario", SqlDbType.Int) { Value = idFormulario };
                return instancia.EjecutarDataSet("SqlDefault", "ObtenerFuncionesPorCampo", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, usuarioLog, "EntFuncionesJS", "ObtenerFuncionesCampo - " + ex.Message);
                return null;
            }
        }

    }
}
