using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudMySql.LogicaNegocios;

namespace CrudMySql.App
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnCargaGrid_Click(object sender, EventArgs e)
        {
            var cadena = "ñ56651ibijnoípóioijnij";

            var boolean = cadena.All(Char.IsLetterOrDigit);
            var lista = new Clientes().ObtenerTabla();
            dataGridView1.DataSource = lista;
        }
    }
}
