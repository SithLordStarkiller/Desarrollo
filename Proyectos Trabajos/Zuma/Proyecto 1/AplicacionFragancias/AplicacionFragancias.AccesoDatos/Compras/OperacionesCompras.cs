using System;
using System.Collections.Generic;
using System.Linq;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Compras
{
    public class OperacionesCompras
    {
        public COM_PROVEEDORES_CONTACTOS InsertaProveedoresContactos(COM_PROVEEDORES_CONTACTOS contactos)
        {
            using (var r = new Repositorio<COM_PROVEEDORES_CONTACTOS>())
            {
                return r.Agregar(contactos);
            }
        }

        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra)
        {
            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                return r.Agregar(ordenCompra);
            }
        }

        public COM_PROVEEDORES InsertaProveedor(COM_PROVEEDORES proveedor)
        {
            using (var r = new Repositorio<COM_PROVEEDORES>())
            {
                return r.Agregar(proveedor);
            }
        }

        public COM_PRODUCTOS_PEDIDOS InsertaProductoOrdencompra(COM_PRODUCTOS_PEDIDOS ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS_PEDIDOS>())
            {
                return r.Agregar(ordenCompra);
            }
        }

        public List<COM_PRODUCTOS> ObtenCatProductos()
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.TablaCompleta();
            }
        }

        public COM_PROVEEDORES ExisteProveedor(string cveProveedor)
        {
            using (var r = new Repositorio<COM_PROVEEDORES>())
            {
                return r.Extraer(e => e.CVEPROVEEDOR == cveProveedor);
            }
        }

        public List<COM_ORDENCOMPRA> ObtenListasOrdenesDeCompra(DateTime inicio, DateTime fin, List<int> status)
        {
            using (var i = new Repositorio<COM_ORDENCOMPRA>())
            {
                return
                    i.Filtro(
                        r =>
                            r.FECHAORDENCOMPRA >= inicio && r.BORRADO == false && r.FECHAORDENCOMPRA <= fin && status.Contains((int)r.IDESTATUSCOMPRA)).ToList();
            }
        }

        public List<COM_PROVEEDORES> ObtenListaProveedores()
        {
            using (var r = new Repositorio<COM_PROVEEDORES>())
            {
                return r.TablaCompleta();
            }
        }

        public COM_ORDENCOMPRA ObtenOrdenCompra(string noOrdenCompra)
        {
            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                return r.Extraer(aux => aux.NOORDENCOMPRA == noOrdenCompra && aux.BORRADO == false);
            }
        }

        public List<COM_PRODUCTOS_PEDIDOS> ObtenListaProductos(COM_PRODUCTOS_PEDIDOS ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS_PEDIDOS>())
            {
                return r.Filtro(aux => aux.NOORDENCOMPRA == ordenCompra.NOORDENCOMPRA && aux.BORRADO == false);
            }
        }

        public List<COM_PRODUCTOS_PEDIDOS> ObtenListaProductos(string ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS_PEDIDOS>())
            {
                return r.Filtro(aux => aux.NOORDENCOMPRA == ordenCompra && aux.BORRADO == false);
            }
        }

        public bool ActualizaProducto(COM_PRODUCTOS producto)
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.Actualizar(producto);
            }
        }

        public bool EliminarOrdenCompra(string noOrdenCompra)
        {
            var ordenCompra = ObtenOrdenCompra(noOrdenCompra);

            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                ordenCompra.BORRADO = true;

                return r.Actualizar(ordenCompra);
            }
        }
    }
}
