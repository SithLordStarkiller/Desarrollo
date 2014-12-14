using System;
using System.Windows.Forms;

namespace Universidad.AplicacionAdministrativa
{
    public partial class Excepcion : Form
    {
        private readonly Exception _error;

        public Excepcion(Exception error)
        {
            InitializeComponent();
            _error = error;
        }

        private void Excepcion_Load(object sender, EventArgs e)
        {
            LlenaExcepcion();
        }

        public void LlenaExcepcion()
        {
            txb_MensageExcepcion.Text = string.Empty;
            txb_MensageExcepcion.Text += Environment.NewLine + @"Fuente: " + _error.Source;
            txb_MensageExcepcion.Text += Environment.NewLine + @"Mensage: " + _error.Message;
            txb_MensageExcepcion.Text += Environment.NewLine + @"Excepcion: " + _error.InnerException;
            txb_MensageExcepcion.Text += Environment.NewLine + @"Pila: " + _error.StackTrace;
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
