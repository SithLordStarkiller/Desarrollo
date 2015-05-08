using System;
using Universidad.Controlador.SVR_Login;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.Login
{
    public class SVC_LoginAdministrativos
    {
        #region Propiedades de la clase

        private readonly S_LoginClient _servicio;

        public SVC_LoginAdministrativos(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new S_LoginClient(configServicios.ObtenBasicHttpBinding(),configServicios.ObtenEndpointAddress(sesion,@"Login_S/","S_Login.svc"));
        }

        #endregion

        #region PruebaServicio

        public delegate void PruebaServicioArgs(bool funciona);

        public event PruebaServicioArgs PruebaServicioFinalizado;

        public void PruebaServicioCompleto()
        {
            _servicio.FuncionaCompleted += _servicio_FuncionaCompleted;
            _servicio.FuncionaAsync();
        }

        void _servicio_FuncionaCompleted(object sender, FuncionaCompletedEventArgs e)
        {
            try
            {
                var funciona = e.Result;
                PruebaServicioFinalizado(funciona);
            }
            catch (Exception)
            {
                PruebaServicioFinalizado(false);
            }
        }

        #endregion

        #region LoginAdministrativo

        public delegate void LoginAdministrativoArgs(US_USUARIOS usuario);

        public event LoginAdministrativoArgs LoginAdministrativosFinalizado;

        public void LoginAdministrativo(string usuario, string contrasena)
        {
            _servicio.LoginAdministradorCompleted += _servicio_LoginAdministradorCompleted;
            _servicio.LoginAdministradorAsync(usuario, contrasena);
        }

        void _servicio_LoginAdministradorCompleted(object sender, LoginAdministradorCompletedEventArgs e)
        {
            try
            {

                //if (e.Result == null) return;

                var resultado = e.Result;
                var usuario = resultado;

                LoginAdministrativosFinalizado(usuario);
            }
            catch (Exception)
            {
                var usuario = new US_USUARIOS()
                {
                    ID_USUARIO = 0,
                    USUARIO = "Problema de conexion con el servidor"
                };
                LoginAdministrativosFinalizado(usuario);
            }
        }

        #endregion

        #region Obten Persona

        public delegate void ObtenNombreCompletoArgs(PER_PERSONAS persona);

        public event ObtenNombreCompletoArgs ObtenNombreCompletoFinalizado;

        public void ObtenNombreCompleto(US_USUARIOS usuario)
        {
            _servicio.ObtenPersonaCompleted += _servicio_ObtenPersonaCompleted;
            _servicio.ObtenPersonaAsync(usuario);
        }

        void _servicio_ObtenPersonaCompleted(object sender, ObtenPersonaCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var resultado = e.Result;
            var persona = resultado;
            ObtenNombreCompletoFinalizado(persona);
        }
        #endregion

        #region Metodos Sincronos

        public PER_PERSONAS ObtenNombreCompletoSincrono(US_USUARIOS persona)
        {
            return _servicio.ObtenPersonas(persona);
        }

        #endregion
    }
}
