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
            var accesoDatos = new LoginA();

            return accesoDatos.LoginPersonas(correoElectronico, contrasena);
        }

        public List<CAT_AREANEGOCIO> ListarAreanegocios()
        {
            var accesoDatos = new LoginA();
            return accesoDatos.ListaAreaNegocios();
        }
    }
}
