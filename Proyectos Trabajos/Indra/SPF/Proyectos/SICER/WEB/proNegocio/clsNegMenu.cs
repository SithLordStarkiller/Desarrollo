using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proDatos;

namespace proNegocio
{
    public class clsNegMenu
    {
        public static List<TreeNode> menuContextual(string tipoOperacion, byte idRol)
        {
            return clsDatMenu.MenuContextual(tipoOperacion, idRol);
        }

        public static List<TreeNode> menuCertificaciones()
        {
            return clsDatMenu.menuCertificaciones();
        }

        public static List<TreeNode> rolPermiso(byte idRol)
        {
            return clsDatMenu.rolPermiso(idRol);
        }
    }
}
