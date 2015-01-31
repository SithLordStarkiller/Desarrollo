using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrudMySql.LogicaNegocios.Usuarios;

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
            var lista = new GestionUsuarios().ObtenTodosUsuarios();
            dataGridView1.DataSource = lista;
        }
    }
}
