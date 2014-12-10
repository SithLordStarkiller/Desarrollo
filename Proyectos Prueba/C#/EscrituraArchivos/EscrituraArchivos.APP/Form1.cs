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
        
        private void btn_CreaDirectorio_Click(object sender, EventArgs e)
        {
            try
            {
                const string path = @"Datos";
                System.IO.Directory.CreateDirectory(path);
                textBox1.Text = @"Se creo Carpeta Nuevo";
            }
            catch (Exception er)
            {
                MuestraExcepcion(er);
            }
        }

        private void MuestraExcepcion(Exception error)
        {
            textBox1.Text = string.Empty;
            textBox1.Text += @"\nFuente: " + error.Source;
            textBox1.Text += @"\nMensage: " + error.Message;
            textBox1.Text += @"\nExcepcion: " + error.InnerException;
            textBox1.Text += @"\nPila: " + error.StackTrace;
        }
    }
}
