using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OperacionesExcel
{
    public partial class Inicio : Form
    {
        private string _ruta;
        private OleDbConnectionStringBuilder _cb;
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            fodCargaExcel.ShowDialog();

            try
            {
                fodCargaExcel.FileOk += fodCargaExcel_FileOk;
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + Environment.NewLine + ex);
            }
        }

        void fodCargaExcel_FileOk(object sender, CancelEventArgs e)
        {
            _ruta = ((OpenFileDialog)(sender)).FileName;
            _cb = new OleDbConnectionStringBuilder { DataSource = _ruta };

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
            textBox1.Text = _ruta;

            fodCargaExcel.FileOk -= fodCargaExcel_FileOk;
        }

        private async void btnCargarExcelOLDB_Click(object sender, EventArgs e)
        {
            tbpVistaTablas.TabPages.Clear();
            btnCargarArchivo.Enabled = false;
            btnCargarExcelOLDB.Enabled = false;
            var a = await CargaExcel();
            btnCargarArchivo.Enabled = true;
            btnCargarExcelOLDB.Enabled = true;

        }

        private async Task<bool> CargaExcel()
        {

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
                    var grid = new DataGridView
                    {
                        DataSource = item,
                        Width = tbpVistaTablas.Width,
                        Height = tbpVistaTablas.Height,
                        ScrollBars = ScrollBars.Both
                    };

                    var tabPage = new TabPage(item.TableName.Replace("$", ""));

                    tabPage.Controls.Add(grid);
                    tbpVistaTablas.TabPages.Add(tabPage);
                }
            }
            return true;
        }
    }
}
