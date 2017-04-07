using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Negocios
{
    public class NegocioUsuario
    {
        public UsuarioModel ValidaUsuario(String dominio, String usuario, String password, bool esCallCenter)
        {
            if (esCallCenter)
            {
                if (!(dominio.ToLower().Equals("mu?ozcc") || dominio.ToLower().Equals("pp") || dominio.ToLower().Equals("infonavit")))
                    return null;
            }

            var ent = new EntUsuario();
            return ent.ValidaUsuario(dominio, usuario, password);
        }
    }
}
