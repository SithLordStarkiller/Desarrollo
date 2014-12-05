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
using EscrituraArchivos.APP.Controles;

namespace EscrituraArchivos.APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ListarDocs_Click(object sender, EventArgs e)
        {
            var gA = new GestionArchivos();
            var texto = gA.CreaDirectorio();
            textBox1.Text = texto;
        }
    }
}
