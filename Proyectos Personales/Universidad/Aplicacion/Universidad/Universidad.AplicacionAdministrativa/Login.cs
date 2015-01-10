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

        public FORM_Login(Sesion sesion)
        {
            _sesion = sesion;

            if (_sesion == null) return;
            if (sesion.RecordarSesion)
            {

                _usuario = _sesion.Usuario;
                _contrasena = _sesion.Contrasena;
                _inicioAutomatico = _sesion.RecordarSesion;

                var login = new SVC_LoginAdministrativos(_sesion);
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
            TopMost = true;
            Focus();
            Show();
        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            try
            {
                _usuario = "ecruzlagunes";//TXB_Usuario.Text;
                _contrasena = "A@141516182235";//TXB_Contrasena.Text;
                _inicioAutomatico = _sesion.RecordarSesion == false ? ckbRecordarSesion.Checked ;

                var login = new SVC_LoginAdministrativos(_sesion);

                login.LoginAdministrativo(_usuario, _contrasena);
                login.LoginAdministrativosFinalizado += Login_LoginAdministrativosFinalizado;
            }
            catch (Exception exception)
            {
                throw;
                //MessageBox.Show((exception.InnerException).ToString());
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
                        _sesion.RecordarSesion = _sesion.RecordarSesion != true;
                        new GestionSesion().ActualizaArchivo(_sesion);
                        var fromInicio = new Inicio(this, _login,_sesion);
                        Hide();
                        fromInicio.Show();
                    }
                        break;
                }
            }

            else
            {
                MessageBox.Show(text: @"El usuario o contraseña no son valido favor de verificar",
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
