using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudMySql.Entidades;

namespace CrudMySql.AccesoDatos.Usuarios
{
    public class GestionUsuarios
    {
        private readonly worldEntities _contexto = new worldEntities();

        public List<us_usuarios> ObtenUsuariosLinq(string usuario, string contrasena)
        {
            using (var aux = new Repositorio<us_usuarios>())
            {
                return aux.Filtro(r => r.USUARIO == usuario && r.CONTRASENA == contrasena);
            }
        }

        public List<us_usuarios> ObtenTodosUsuariosLinq()
        {
            using (var aux = new Repositorio<us_usuarios>())
            {
                return aux.TablaCompleta();
            }
        }
    }
}
