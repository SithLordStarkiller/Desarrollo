using System;
using System.Windows.Forms;
using Universidad.AplicacionAdministrativa.Vistas;
using Universidad.Controlador.Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;


namespace Universidad.AplicacionAdministrativa
{
    public partial class FORM_Login : Form
    {
        private US_USUARIOS _login;
        private readonly Sesion _sesion;
        private string _contrasena, _usuario;
        private bool _inicioAutomatico;
        private bool _recordarDatos;

        public FORM_Login(Sesion sesion)
        {
            _sesion = sesion;

            if (_sesion == null) return;

            if (sesion.RecordarSesion)
            {

                _usuario = _sesion.Usuario;
                _contrasena = _sesion.Contrasena;
                _inicioAutomatico = _sesion.RecordarSesion;

                var login = new SvcLogin(_sesion);
                login.LoginAdministrativo(_sesion.Usuario, _sesion.Contrasena);
                login.LoginAdministrativosFinalizado += Login_LoginAdministrativosFinalizado;

                Visible = false;
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                InitializeComponent();
            }

        }

        private void FORM_Login_Load(object sender, EventArgs e)
        {
            if (_sesion.RecordarContrasena)
            {
                TXB_Usuario.Text = _sesion.Usuario;
                TXB_Contrasena.Text = _sesion.Contrasena;
                _recordarDatos = ckbRecodarDatos.Checked = true;
            }
            Focus();
            Show();
        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            try
            {
                _usuario = TXB_Usuario.Text;
                _contrasena = TXB_Contrasena.Text;
                _inicioAutomatico = !_sesion.RecordarSesion && ckbRecordarSesion.Checked;
                _recordarDatos = ckbRecodarDatos.Checked;

                var login = new SvcLogin(_sesion);

                login.LoginAdministrativo(_usuario, _contrasena);
                login.LoginAdministrativosFinalizado += Login_LoginAdministrativosFinalizado;
            }
            catch (Exception exception)
            {
                new Excepcion(exception).Show();
            }
        }

        void Login_LoginAdministrativosFinalizado(US_USUARIOS usuario)
        {
            _login = usuario;

            if (_login != null && _login.ID_USUARIO != 0)
            {
                switch (_login.ID_ESTATUS_USUARIOS)
                {
                    case 1:
                        {
                            _sesion.Usuario = string.IsNullOrEmpty(_usuario) ? _sesion.Usuario : _usuario;
                            _sesion.Contrasena = string.IsNullOrEmpty(_contrasena) ? _sesion.Contrasena : _contrasena;
                            _sesion.RecordarSesion = _inicioAutomatico;
                            _sesion.RecordarContrasena = _recordarDatos;
                            new GestionSesion().ActualizaArchivo(_sesion);
                            var fromInicio = new Inicio(this, _login, _sesion);
                            Hide();
                            fromInicio.Show();
                        }
                        break;

                    case 2:
                        {
                            MessageBox.Show(text: @"la cuenta del usuario " + _sesion.Usuario + @" se encuentra suspendida por favor informe al administrador del sistema",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Exclamation);
                            _sesion.RecordarSesion = false;
                            _sesion.RecordarContrasena = false;
                            new GestionSesion().ActualizaArchivo(_sesion);
                            Dispose();
                        }
                        break;
                    case 3:
                        {
                            MessageBox.Show(text: @"la cuenta del usuario " + _sesion.Usuario + @" se encuentra cancelada por favor verifique el precedimiento de activacion de la cuenta con administracion",
                        caption: @"Informe de usuario", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                            _sesion.RecordarSesion = false;
                            _sesion.RecordarContrasena = false;
                            new GestionSesion().ActualizaArchivo(_sesion);
                            Dispose();
                        }
                        break;
                }
            }

            else if (_login != null && (_login.ID_USUARIO == 0 && _login.USUARIO == "Problema de conexion con el servidor"))
            {
                MessageBox.Show(text: @"Hay un problema de conexion con el servidor, reporte el problema con el area de sistemas",
                        caption: @"Error en el sistema", buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                _sesion.RecordarSesion = false;
                _sesion.RecordarContrasena = false;
                new GestionSesion().ActualizaArchivo(_sesion);
                Dispose();
            }

            else
            {
                MessageBox.Show(text: @"El usuario o contraseña no son correctos favor de verificar",
                    caption: @"Error al verificar Login", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
        }

        private void BTN_Salir_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
