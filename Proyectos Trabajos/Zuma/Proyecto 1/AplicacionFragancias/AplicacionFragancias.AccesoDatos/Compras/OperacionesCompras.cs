using System.Collections.Generic;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Compras
{
    public class OperacionesCompras
    {
        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra)
        {
            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                return r.Agregar(ordenCompra);
            }
        }

        public COM_PRODUCTOS InsertaProductoOrdencompra(COM_PRODUCTOS ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.Agregar(ordenCompra);
            }
        }

        public COM_ORDENCOMPRA ObtenOrdenCompra(string noOrdenCompra)
        {
            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                return r.Extraer(aux => aux.NOORDENCOMPRA == noOrdenCompra && aux.BORRADO == false);
            }
        }

        public List<COM_PRODUCTOS> ObtenListaProductos(COM_ORDENCOMPRA ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.Filtro(aux => aux.NOORDENCOMPRA == ordenCompra.NOORDENCOMPRA && aux.BORRADO == false);
            }
        }

        public bool ActualizaProducto(COM_PRODUCTOS producto)
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.Actualizar(producto);
            }
        }
    }
}
