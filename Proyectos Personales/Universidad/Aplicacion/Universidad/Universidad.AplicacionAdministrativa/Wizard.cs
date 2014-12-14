using System;
using System.Windows.Forms;
using Universidad.Controlador.Login;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.AplicacionAdministrativa
{
    public partial class Wizard : Form
    {
        public Wizard()
        {
            InitializeComponent();
        }

        private void brn_Aceptar_Click(object sender, EventArgs e)
        {
            ActualizaConfig();
        }

        private void ActualizaConfig()
        {
            var ruta = txb_RutaServicio.Text;
            var sesion = new Sesion { Conexion = ruta, Contrasena = null, RecordarSesion = false, Usuario = null };
            var gestionConfiguracion = new GestionSesion();
            gestionConfiguracion.ActualizaArchivo(sesion);
            PuebaConexion(sesion);
        }

        private void PuebaConexion(Sesion sesion)
        {
            var login = new SVC_LoginAdministrativos(sesion);

            login.PruebaServicioCompleto();
            login.PruebaServicioFinalizado += login_PruebaServicioFinalizado;
        }

        void login_PruebaServicioFinalizado(bool funciona)
        {
            if (funciona)
            {
                MessageBox.Show(
                    @"Se logro contartar con el servicios " + Environment.NewLine +
                    @" La aplicacion se cerrara debes ejecutarla nuevamente",
                    @"Se establecio conexion",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            else
            {
                txb_RutaServicio.Text = string.Empty;
                var respuesta =
                    MessageBox.Show(
                        @"No se logro establecer conexion con el servidor " + Environment.NewLine +
                        @" Deceas salir de la aplicacion?",
                        @"Error en la conexion",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            }
        }
    }
}
