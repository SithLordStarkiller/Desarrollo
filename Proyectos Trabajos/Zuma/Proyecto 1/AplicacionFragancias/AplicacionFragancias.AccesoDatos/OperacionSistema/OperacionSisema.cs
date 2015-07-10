using System.Collections.Generic;
using System.Linq;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.OperacionSistema
{
    public class OperacionSisema
    {
        private readonly FraganciasEntities _contexto = new FraganciasEntities();
        public US_USUARIOS ObtenUsuario(string usuario, string contrasena)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Extraer(a => a.USUARIO == usuario && a.CONTRASENA == contrasena && a.BORRADO == false);
            }
        }

        public List<SIS_MENUARBOL> ObtenListaMenuArbol(US_USUARIOS usuario)
        {
            var lista = (from sma in _contexto.SIS_MENUARBOL
                         join spm in _contexto.SIS_PERFILES_MENU on sma.IDMENU equals spm.IDMENU
                         join up in _contexto.US_PERFILES on spm.IDPERFIL equals up.IDPERFIL
                         where up.IDUSUARIOS == usuario.IDUSUARIOS && sma.BORRADO == false
                         select new
                         {
                             sma.IDMENU,
                             sma.IDMENUPADRE,
                             sma.NOMBRE,
                             sma.DIRECCION,
                             sma.BORRADO

                         }).AsEnumerable().Select(x => new SIS_MENUARBOL
                         {
                             IDMENU = x.IDMENU,
                             IDMENUPADRE = x.IDMENUPADRE,
                             NOMBRE = x.NOMBRE,
                             DIRECCION = x.DIRECCION,
                             BORRADO = x.BORRADO
                         }).ToList();

            return lista;
        }
    }
}
