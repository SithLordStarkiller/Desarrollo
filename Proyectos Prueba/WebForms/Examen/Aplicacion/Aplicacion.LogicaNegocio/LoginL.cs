using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Entidades;
using Aplicacion.AccesoDatos;

namespace Aplicacion.LogicaNegocio
{
    public class LoginL
    {
        public PER_PERSONAS LoginPersonas(string correoElectronico, string contrasena)
        {
            return new LoginA().LoginPersonas(correoElectronico, contrasena);
        }

        public List<CAT_AREANEGOCIO> ListarAreanegocios()
        {
            return new LoginA().ListaAreaNegocios();
        }

        public void NuevoUsuario(PER_PERSONAS empleado)
        {
            new LoginA().NuevoUsuario(empleado);
        }
    }
}
