using System.Collections.Generic;
using System.Linq;
using PubliPayments.Entidades;

namespace PubliPayments.Models
{
    public class UsuariosMapa
    {
        public List<UsuarioMapa> ObtenerUsuariosMapa(int usuario, int dominio)
        {
            var context = new SistemasCobranzaEntities();
                 
            var listaUsuario = from u in context.VUsuarios
                                where u.idDominio == dominio
                                && u.idPadre == usuario
                                && u.Estatus == 1
                                orderby u.Usuario
                                select u;

            var lista =
                listaUsuario.Select(
                    hijo => new UsuarioMapa {IdUduario = hijo.idUsuario, Nombre = hijo.Nombre, Usuario = hijo.Usuario})
                    .ToList();

            return lista;
        }
    }
}