using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proSeguridad;
using proEntidad;
using proDatos;

namespace proNegocio
{
    public class clsNegUsuario 
    {
        public static clsEntSisAutenticacion consultarPermisoAdmin(ref clsEntSesion objSession)
        {
            #region Permisos
            string message = string.Empty;
            bool response = false;
            int dbError = 0;
            try
            {
                dbError = clsDatUsuario.consultarPermisoAdmin(ref objSession);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (message == string.Empty)
            {
                switch (dbError)
                {
                    case 18456:
                        message = "Nombre de Usuario y/o Contraseña Incorrecto.";
                        break;
                    case 18486:
                        message = "La cuenta ha sido bloqueada";
                        break;
                }
            }


            if (message == string.Empty)
            {
                if (objSession.Usuario.IdUsuario != 0)
                {
                    objSession.Inicio = DateTime.Now;
                    objSession.Ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    objSession.SessionId = System.Web.HttpContext.Current.Session.SessionID;
                    objSession.Estatus = clsEntSesion.TipoEstatus.Activa;

                    System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] = objSession;
                    int iResultado = 0;

                    try
                    {
                        iResultado = clsDatSesion.iniciarSesion(objSession);
                    }
                    catch (Exception)
                    {
                        message = "Ha ocurrido un error, inténtelo más tarde.";
                    }

                    switch (iResultado)
                    {
                        case 0:
                            message = "El usuario ya ha iniciado sesión.";
                            break;
                        case 1:
                            message = "app.view.Administracion.main.mainViewAdmin";
                            response = true;
                            break;
                    }
                }
                else
                {
                    message = "Usuario no registrado";
                }

            }
            #endregion

            #region Response
            clsEntSisAutenticacion objResponse = new clsEntSisAutenticacion
            {
                response = response,
                message = message,
                objSesion = objSession
            };

            return objResponse;
            #endregion
        }
        public static clsEntResponseAutenticaExamen consultarPermisoUsuarioExamen(clsEntSesion objSession)
        {
            #region Permisos
            string message = string.Empty;
            int dbError = 0;
            int idRegistro=0;
            try
            {
                if (System.Web.HttpContext.Current.Session["inicioSesion"] == null)
                {
                    //if(System.Web.HttpContext.Current.Application[objSession.Usuario.UsuLogin]==null)
                        dbError = clsDatUsuario.consultarPermisoUsuarioExamen(objSession, ref idRegistro);
                    //else
                    //    dbError = 6;
                }
                else {
                    dbError = 5; //El usuario ya está logueado
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (message == string.Empty && (dbError == 1 || dbError == 2))
            { 
                    try
                    {
                       System.Web.HttpContext.Current.Session["inicioSesion"] = objSession.Usuario.UsuLogin;
                       //System.Web.HttpContext.Current.Application[objSession.Usuario.UsuLogin] = true;
                       clsDatSesion.iniciarSesionExamen(idRegistro);
                       //clsDatUsuario.consultaDatosUsuarioExamen(idRegistro, ref objSession);
                      
                    }
                    catch (Exception ex)
                    {

                        message = ex.Message;
                    }
            }
            #endregion

            #region Response
            clsEntResponseAutenticaExamen objResponse = new clsEntResponseAutenticaExamen
            {
                response = dbError,
                message = message,
                idRegistro = idRegistro,
                nombreUsuario=objSession.Usuario.UsuNombre+ " " + objSession.Usuario.UsuPaterno+ " " + objSession.Usuario.UsuMaterno
            };

            return objResponse;
            #endregion
        }

        public static string consultarPermisoCripter(string cripter)
        {
            string reponse = string.Empty;
            try
            {
                cripter = clsSegRijndaelSimple.Decrypt(cripter);
                clsEntSesion objSession = new clsEntSesion();
                objSession.Usuario = new clsEntUsuario();
                objSession.Usuario.UsuLogin = cripter.Split('|')[0];
                objSession.Usuario.UsuContrasenia = clsSegRijndaelSimple.Encrypt(cripter.Split('|')[1]);
                clsEntSisAutenticacion objResponse = new clsEntSisAutenticacion();

                objResponse = clsNegUsuario.consultarPermisoAdmin(ref objSession);
                reponse = objResponse.response + "|" +
                    (objResponse.objSesion.Usuario.UsuPaterno + " " + objResponse.objSesion.Usuario.UsuMaterno + " " + objResponse.objSesion.Usuario.UsuNombre) + "|" +
                    objResponse.objSesion.Usuario.IdEmpleado + "|" +
                    objResponse.objSesion.Usuario.Perfil.PerDescripcion;
            }
            catch (Exception ex)
            {
                reponse = false + "|" + ex.Message;
            }

            return reponse;
        }
        

    }
}
