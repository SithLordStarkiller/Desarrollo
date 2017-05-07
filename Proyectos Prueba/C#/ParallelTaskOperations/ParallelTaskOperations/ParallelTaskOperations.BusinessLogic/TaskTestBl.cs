namespace ParallelTaskOperations.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Threading.Tasks;

    public class TaskTestBl
    {
        public void TestCancelTask()
        {
            var listNumbers = new List<int>();

            for (var item = 0; item < 100; item++)
            {
                listNumbers.Add(item);
            }

            Parallel.ForEach(listNumbers, numbre =>
            {
                lock (listNumbers)
                {
                    
                }
            });
        }
        private async Task<bool> CargaExcel()
        {
            var _ruta = "";
            var _cb = new OleDbConnectionStringBuilder { DataSource = _ruta };

            if (Path.GetExtension(_ruta).ToUpper() == ".XLS")
            {
                _cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                _cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
            }
            else if (Path.GetExtension(_ruta).ToUpper() == ".XLSX")
            {
                _cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                _cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
            }

            using (var conn = new OleDbConnection(_cb.ConnectionString))
            {
                conn.Open();

                var hojas = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                var excelSheets = (new String[hojas.Rows.Count]);
                var nombreHojas = new List<string>();

                var i = 0; // Add the sheet name to the string array. 

                foreach (DataRow row in hojas.Rows)
                {
                    nombreHojas.Add(excelSheets[i] = row["TABLE_NAME"].ToString());
                    i++;
                }

                var tablas = new List<DataTable>();

                using (var cmd = conn.CreateCommand())
                {
                    foreach (var items in nombreHojas)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM [" + items + "]";
                        var da = new OleDbDataAdapter(cmd);
                        var dt = new DataTable(items);
                        da.Fill(dt);
                        tablas.Add(dt);
                    }
                }

                foreach (var item in tablas)
                {
                }
            }
            return true;
        }

    }
}
