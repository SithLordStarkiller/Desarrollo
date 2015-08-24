using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Universidad.RespaldoCatalogosDb
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            EnlistarInstaciasSqlServer();
        }

        private void EnlistarInstaciasSqlServer()
        {
            var instance =System.Data.Sql.SqlDataSourceEnumerator.Instance;
            var dataTable = instance.GetDataSources();
            foreach (var item in dataTable.Rows)
            {
                
            }
            //cbxInstacias.
        }

        private void btnBuscarExcel_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();

            string[] files = Directory.GetFiles(fbd.SelectedPath);

            foreach (var file in files)
            {
                txtListaExcel.Text += file;
            }
            //txtUrlCarpetaExcel.Text = new
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
