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

        public FORM_Login(Sesion sesion)
        {
            this._sesion = sesion;
            InitializeComponent();
        }

        private void FORM_Login_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = "ecruzlagunes";//TXB_Usuario.Text;
                var contrasena = "A@141516182235";//TXB_Contrasena.Text;

                var Login = new SVC_LoginAdministrativos(_sesion);

                Login.LoginAdministrativo(usuario, contrasena);
                Login.LoginAdministrativosFinalizado += Login_LoginAdministrativosFinalizado;

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
