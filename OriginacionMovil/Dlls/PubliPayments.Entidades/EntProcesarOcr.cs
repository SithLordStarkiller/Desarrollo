using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades
{
    /// <summary>
    /// Entidad para el manejo de las imagenes a procesar por OCR
    /// </summary>
    public class EntProcesarOcr
    {
        /// <summary>
        /// Obtiene los numeros de ordenes son convenios y que fueron autorizados por el conyuge o el acreditado
        /// </summary>
        /// <param name="familiar">True = indica que va a regresar los creditos gestionados por el conyuge y 
        /// false los creditos gestionados por el acreditado </param>
        /// <returns>Regresa un dataset con los registros encontrados</returns>
        public DataSet ObtenerOrdenesPorAceptacionConvenio(bool familiar)
        {
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@Familiar", SqlDbType.Bit) { Value = familiar };
            return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "ObtenerOrdenesPorAceptacionConvenio",
                parametros);
        }

        /// <summary>
        /// Obtiene un listado de las imagenes y los nombres de los acreditados para que sean procesados por el OCR
        /// </summary>
        /// <returns>Regresa un dataset con los registros encontrados</returns>
        public DataSet ObtenerImagenesOcr()
        {
            var parametros = new SqlParameter[0];
            return ConnectionDB.Instancia.EjecutarDataSet("SqlDefault", "ObtenerImagenesOCR",
                parametros);
        }
    }
}
