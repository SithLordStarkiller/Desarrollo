using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenCsi.DataAccess;
using ExamenCsi.Entities;

namespace ExamenCsi.BusinessLogic
{
    public class UsuariosB
    {
        public int InsertarUsuario(UsUsuario usuario)
        {
            return new SqlController().InsertarUsuario(usuario);
        }

        public List<UsUsuario> UsuariosObtenerTodos()
        {
            return new SqlController().UsuariosObtenerTodos();
        }
    }
}
