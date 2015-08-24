using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Universidad.RespaldoCatalogosDb.LogicaNegocios;

namespace Universidad.RespaldoCatalogosDb.Vistas
{
    public partial class CargaDatos : Form
    {
        private List<String> _listaArcivos;
        private string _urlExcel;
        private readonly OleDbConnection _service;
        public CargaDatos(SqlConnectionStringBuilder connectionString, List<String> listaArchivos, string urlDirectory)
        {
            InitializeComponent();

            _listaArcivos = listaArchivos;
            _urlExcel = urlDirectory;
            var connection = new OleDbConnections(connectionString);
            _service = connection.Connect();
        }

        private void CargaDatos_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            _service.Close();
        }
    }
}
