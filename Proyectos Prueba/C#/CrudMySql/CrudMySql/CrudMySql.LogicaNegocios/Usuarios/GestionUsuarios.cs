using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudMySql.AccesoDatos.Usuarios;
using CrudMySql.Entidades;

namespace CrudMySql.LogicaNegocios.Usuarios
{
    public class GestionUsuarios
    {
        public List<us_usuarios> ObtenTodosUsuarios()
        {
            return new AccesoDatos.Usuarios.GestionUsuarios().ObtenTodosUsuariosLinq();
        }
    }
}
