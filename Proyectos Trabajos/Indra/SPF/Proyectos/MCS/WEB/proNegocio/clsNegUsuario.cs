using System;
using System.Data;

namespace SICOGUA.Seguridad
{
    public class clsNegUsuario
    {
        public static void consultarPermiso(ref clsEntSesion objSesion)
        {
            DataTable dt = clsDatUsuario.consultarPermiso(objSesion);

            objSesion.usuario.Perfil = new clsEntPerfil();

            if(dt.Rows.Count == 1)
            {
                objSesion.usuario.IdUsuario = Convert.ToInt32(dt.Rows[0]["idUsuario"].ToString());
                objSesion.usuario.IdEmpleado = (Guid)(dt.Rows[0]["idEmpleado"]);
                objSesion.usuario.Perfil.IdPerfil = Convert.ToInt16(dt.Rows[0]["idPerfil"].ToString());
                objSesion.usuario.Perfil.PerDescripcion = dt.Rows[0]["perDescripcion"].ToString();
                objSesion.usuario.UsuPaterno = (string)dt.Rows[0]["empPaterno"];
                objSesion.usuario.UsuMaterno = (string)dt.Rows[0]["empMaterno"];
                objSesion.usuario.UsuNombre = (string)dt.Rows[0]["empNombre"];
            }
        }

        public static string crearUsuario(clsEntUsuario objUsuario, clsEntSesion objSesion)
        {
            string mensaje = null;

            try
            {
                switch(clsDatUsuario.crearUsuario(objUsuario, objSesion))
                {
                    case 0:
                        mensaje = "Operación finalizada correctamente.";
                        break;
                    case 1:
                        mensaje = "La Contraseña no cumple el nivel de seguridad requerido.";
                        break;
                    case 2:
                        mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
                        break;
                    case 3:
                        mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
                        break;
                    case 4:
                        mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
                        break;
                    case 5:
                        mensaje = "El nombre de usuario ya existe.";
                        break;
                    case 6:
                        mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
                        break;
                    case 7:
                        mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
                        break;
                }
            }
            catch (Exception)
            {
                mensaje = "Ha ocurrido un error, inténtelo nuevamente.";
            }

            return mensaje;
        }
    }
}
