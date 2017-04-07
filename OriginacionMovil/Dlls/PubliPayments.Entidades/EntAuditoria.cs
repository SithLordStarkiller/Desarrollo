using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades
{
    public class EntAuditoria
    {
        /// <summary>
        /// Obtiene los combos para la auditoria de imágenes
        /// </summary>
        /// <param name="conexionBd">Conexión de Bd a la cual se conectara</param>
        /// <param name="idCombo"></param>
        /// <param name="tipoCombo"></param>
        /// <param name="delegacion"></param>
        /// <param name="despacho"></param>
        /// <param name="supervisor"></param>
        /// <returns></returns>
        public DataSet ObtenerCombosAutorizaImagenes(string conexionBd,string idCombo, int tipoCombo, string delegacion, int despacho, string supervisor)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@idCombo", SqlDbType.VarChar, 20) { Value = idCombo };
            parametros[1] = new SqlParameter("@tipoCombo", SqlDbType.Int) { Value = tipoCombo };
            parametros[2] = new SqlParameter("@delegacion", SqlDbType.VarChar, 4) { Value = delegacion };
            parametros[3] = new SqlParameter("@despacho", SqlDbType.Int) { Value = despacho };
            parametros[4] = new SqlParameter("@supervisor", SqlDbType.Int) { Value = supervisor };
            return instancia.EjecutarDataSet(conexionBd, "ObtenerCombos_AutorizaImagenes", parametros);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conexionBd"></param>
        /// <param name="idAuditoriaGestion"></param>
        /// <param name="idUsuario"></param>
        /// <param name="credito"></param>
        /// <param name="autorizado"></param>
        /// <returns></returns>
        public DataSet InsertaAuditoriaImagenes(string conexionBd, int idAuditoriaGestion, int idUsuario, string credito, string autorizado)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@idAuditoriaImagenes", SqlDbType.Int) { Value = idAuditoriaGestion };
            parametros[1] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
            parametros[2] = new SqlParameter("@num_credito", SqlDbType.VarChar, 150) { Value = credito };
            parametros[3] = new SqlParameter("@resultado", SqlDbType.Int) { Value = autorizado };
            return instancia.EjecutarDataSet(conexionBd, "InsertaAuditoriaImagenes", parametros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAuditoriaImagenes"></param>
        /// <param name="imagen"></param>
        /// <param name="resultado"></param>
        /// <param name="conexionBd"></param>
        public void InsertaAuditoriaCamposImagen(string idAuditoriaImagenes, string imagen, string resultado, string conexionBd)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@idAuditoriaImagenes", SqlDbType.Int) { Value = idAuditoriaImagenes };
            parametros[1] = new SqlParameter("@imagen", SqlDbType.VarChar) { Value = imagen };
            parametros[2] = new SqlParameter("@resultado", SqlDbType.Int) { Value = resultado };
          
             instancia.EjecutarDataSet(conexionBd, "InsertaAuditoriaCamposImagen", parametros);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuarioAuditoria">id usuario que esta solicitando la información</param>
        /// <param name="delegacion"></param>
        /// <param name="despacho"></param>
        /// <param name="supervisor"></param>
        /// <param name="gestor"></param>
        /// <param name="dictamen"></param>
        /// <param name="status"></param>
        /// <param name="autoriza"></param>
        /// <param name="tipoFormulario"></param>
        /// <param name="valorOcr">Valor de OCR para realizar filtrado</param>
        /// <param name="conexionBd"></param>
        /// <returns></returns>
        public DataSet ObtenerTablaAuditoriaImagenes(string idUsuarioAuditoria,string delegacion,string despacho,string supervisor,string gestor,string dictamen,string status,string autoriza,string tipoFormulario,string valorOcr,string conexionBd)
        {
               var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[10]; 
            parametros[0] = new SqlParameter("@delegacion", SqlDbType.VarChar,4) { Value = delegacion };
            parametros[1] = new SqlParameter("@despacho", SqlDbType.VarChar, 4) { Value = despacho };
            parametros[2] = new SqlParameter("@supervisor", SqlDbType.VarChar, 4) { Value = supervisor };
            parametros[3] = new SqlParameter("@gestor", SqlDbType.VarChar, 4) { Value = gestor };
            parametros[4] = new SqlParameter("@dictamen", SqlDbType.VarChar, 150) { Value = dictamen };
            parametros[5] = new SqlParameter("@status", SqlDbType.VarChar, 4) { Value = status };
            parametros[6] = new SqlParameter("@autoriza", SqlDbType.VarChar, 4) { Value = autoriza };
            parametros[7] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar, 10) { Value = tipoFormulario };
            parametros[8] = new SqlParameter("@valorOcr", SqlDbType.VarChar, 150) { Value = valorOcr };
            parametros[9] = new SqlParameter("@idUsuarioAuditoria", SqlDbType.VarChar, 4) { Value = idUsuarioAuditoria };
         
            return  instancia.EjecutarDataSet(conexionBd, "ObtenerTablaAuditoriaImagenes", parametros);
        }

        public DataSet ObtenerDatosAuditoria(int idAuditoriaImagenes)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idAuditoriaImagenes", SqlDbType.Int) { Value = idAuditoriaImagenes };
            return instancia.EjecutarDataSet("SqlDefault", "ObtenerDatosAuditoria", parametros);
        }
    }
}
