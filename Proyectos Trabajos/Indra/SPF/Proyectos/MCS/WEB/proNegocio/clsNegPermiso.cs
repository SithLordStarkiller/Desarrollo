using System;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;

namespace SICOGUA.Seguridad
{
    public class clsNegPermiso
    {
        public static void aplicarPermisos(Menu mnuPrincipal, clsEntPermiso objPermiso, clsEntSesion objSesion)
        {
            foreach (MenuItem item in mnuPrincipal.Items)
            {
                item.Enabled = clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, Convert.ToInt16(item.Value), objSesion);
                permisosRecursivos(item, objPermiso, objSesion);
            }
        }

        public static void aplicarPermisosBotones(clsEntPermiso objPermiso, clsEntSesion objSesion)
        {
            //foreach (MenuItem item in mnuPrincipal.Items)
            //{
                //item.Enabled = clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, Convert.ToInt16(item.Value), objSesion);
                //permisosRecursivos(item, objPermiso, objSesion);
            //}
        }

        private static void permisosRecursivos(MenuItem miOpcion, clsEntPermiso objPermiso, clsEntSesion objSesion)
        {
            foreach (MenuItem item in miOpcion.ChildItems)
            {
                item.Enabled = clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, Convert.ToInt16(item.Value), objSesion);
                permisosRecursivos(item, objPermiso, objSesion);
            }
        }

        public static bool permitirPagina(clsEntPermiso objPermiso, string pagina, clsEntSesion objSesion)
        {
            return clsDatPermiso.consultarPermiso(objPermiso.IdPerfil, pagina, objSesion);
        }
    }
}
