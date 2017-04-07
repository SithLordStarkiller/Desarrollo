using PubliPayments.Utiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Entidades
{
    public class EntArchivos
    {
        /// <summary>
        /// Obtiene los archivos zip y txt con error
        /// </summary>
        /// <param name="tipo">Tipo de archivo</param>
        /// <param name="errorCompleto">Si tienen error</param>
        /// <returns>List<ArchivosModel></returns>
//        public List<ArchivosModel> ObtenerArchivosConError(ref string tipo, ref bool errorCompleto)
//        {
//            try
//            {
//                var cnn = ConnectionDB.Instancia;
//                String sqlQuery = @"SELECT a.id, a.Archivo, a.Tipo, a.Tiempo, a.Registros, a.Fecha, a.Estatus, " +
//                                  (errorCompleto ? "b.Error " : "b.id_archivo as Error ") +
//                                  @"FROM Archivos a WITH (NOLOCK)
//                                LEFT OUTER JOIN ArchivosError b  WITH (NOLOCK) on a.id = b.id_archivo
//                                WHERE a.tipo = 'rar' or a.Tipo = '" + tipo + @"' " +
//                                  " ORDER BY a.id DESC";
//                var result = cnn.EjecutarDataSet("SqlDefault", sqlQuery);

//                var lista=result.Tables[0].ToList<ArchivosModel>();
//                return lista;
//            }
//            catch (Exception ex)
//            {
//                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en ObtenerArchivosConError: " + ex.Message);
//                return new List<ArchivosModel>();
//            }
//        }
    }
}
