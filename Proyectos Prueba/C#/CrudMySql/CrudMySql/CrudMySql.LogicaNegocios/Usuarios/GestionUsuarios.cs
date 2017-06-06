using System.Data;

namespace CrudMySql.LogicaNegocios
{
    using Entidades;
    public class Clientes
    {
        public DataTable ObtenerTabla()
        {
            return new EntClientesBroxel().ObtenerTabla();
        }
    }
}
