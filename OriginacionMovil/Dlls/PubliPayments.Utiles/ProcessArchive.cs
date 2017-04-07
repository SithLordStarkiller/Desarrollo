using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace PubliPayments.Utiles
{
   public class ProcessArchive
    {
        /// <summary>
        /// Carga un archivo Excel y lo convierte a un archivo .txt
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo que se va a generar</param>
        /// <param name="rutaGenerar">Ruta de destino donde se generara el nuevo archivo</param>
        /// <returns>Ruta del nuevo archivo .txt</returns>
        public static string CargarExcel(string rutaArchivo, string rutaGenerar)
        { 
            var nombreArchivo = "";
            try
            {              
                var cb = new OleDbConnectionStringBuilder { DataSource = rutaArchivo };
                var extension = Path.GetExtension(rutaArchivo);

                if (extension != null)
                {
                    nombreArchivo = rutaGenerar + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Second + Path.GetFileNameWithoutExtension(rutaArchivo) + ".txt";
                    switch (extension.ToUpper())
                    {

                        case ".XLS":
                            cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                            cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=1;");
                            break;
                        case ".XLSX":
                            cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                            cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=1;");
                            break;
                    }
                    var dTable = new DataTable();

                    using (var conn = new OleDbConnection(cb.ConnectionString))
                    {
                        //Abrimos la conexión
                        conn.Open();

                        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        var tableName = "";

                        if (dtSheet != null)
                            foreach (DataRow drSheet in dtSheet.Rows)
                            {
                                if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                                {
                                    tableName = drSheet["TABLE_NAME"].ToString();
                                    break;
                                }
                            }
                        using (OleDbCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM [" + tableName + "]";
                            var da = new OleDbDataAdapter(cmd);
                            da.Fill(dTable);
                        }
                        var columnsName = dTable.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();
                        //Cerramos la conexión
                        conn.Close();

                        using (var wr = new StreamWriter(nombreArchivo))
                        {
                            wr.WriteLine(string.Join("|", columnsName));
                            foreach (var row in dTable.Rows)
                            {
                                var line = string.Join("|", ((DataRow)(row)).ItemArray);
                                line = line.Replace('\r', ' ').Replace('\n', ' ').Replace("12:00:00 a.m.", "");
                                if (line.Replace('|', ' ').Trim().Length > 5)
                                {
                                    wr.WriteLine(line);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ProcessArchive", "CargarExcel: Error  - "+e.Message);
                return "";
            }
            return nombreArchivo;
        }
    }
}
