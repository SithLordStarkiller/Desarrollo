using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntCredito
    {
        public DataSet ObtenerCreditoPorOrden(int idOrden)
        {
            var guidTiempos = Tiempos.Iniciar();

            var context = new SistemasCobranzaEntities();
            var cred = (from o in context.Ordenes
                join c in context.Creditos on o.num_Cred equals c.CV_CREDITO
                where o.idOrden == idOrden
                select new
                {
                    o.num_Cred,
                    c.TX_NOMBRE_DESPACHO
                }).First();

            Tiempos.Terminar(guidTiempos);

            return ObtenerCredito(cred.num_Cred, cred.TX_NOMBRE_DESPACHO);
        }

        public DataSet ObtenerCredito(string credito, string despacho)
        {
            var guidTiempos = Tiempos.Iniciar();

            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sql = "SELECT * FROM Creditos WHERE CV_Credito = '" + credito + "'  AND TX_NOMBRE_DESPACHO = '" +
                      despacho + "'";
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);

            Tiempos.Terminar(guidTiempos);
            
            return ds;
        }
        
        /// <summary>
        /// Inserta el credito, genera la orden y la asocia para poder generarles las respuestas cuando es originado en movíl
        /// </summary>
        /// <returns>Regresa un booleano si se pudo realizar o no la operación</returns>
        public bool InsertaCreditoOrden(string credito, string usuario, int idUsuarioPadre,
            int idUsuarioAlta, int idDominio, string ruta,string canal,string etiqueta,string cvContrato)
        {
            var guidTiempos = Tiempos.Iniciar();

            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand("InsertaCreditoOrden", cnn);
            var parametros = new SqlParameter[9];

            parametros[0] = new SqlParameter("@credito", SqlDbType.NVarChar, 50) {Value = credito};
            parametros[1] = new SqlParameter("@usuario", SqlDbType.VarChar,150) {Value = usuario};
            parametros[2] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
            parametros[3] = new SqlParameter("@idUsuarioAlta ", SqlDbType.Int) {Value = idUsuarioAlta};
            parametros[4] = new SqlParameter("@idDominio", SqlDbType.Int) {Value = idDominio};
            parametros[5] = new SqlParameter("@ruta", SqlDbType.VarChar, 10) {Value = ruta};
            parametros[6] = new SqlParameter("@canal", SqlDbType.VarChar, 1) { Value = canal };
            parametros[7] = new SqlParameter("@etiqueta", SqlDbType.VarChar) { Value = etiqueta };
            parametros[8] = new SqlParameter("@CV_CONTRATO", SqlDbType.VarChar) { Value = cvContrato };

            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            bool ok = ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1;

            Tiempos.Terminar(guidTiempos);

            return ok;
        }
    }
}
