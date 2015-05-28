using System.Collections.Generic;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Facturacion
{
    public class OperacionesCatalogosFacturacion
    {
        public List<FAC_CAT_CONDICIONES_PAGO> ObtenCatalogoTipoPago()
        {
            using (var i = new Repositorio<FAC_CAT_CONDICIONES_PAGO>())
            {
                return i.TablaCompleta();
            }
        }
        public List<FAC_CAT_MONEDA> ObtenCatalogoMoneda()
        {
            using (var i = new Repositorio<FAC_CAT_MONEDA>())
            {
                return i.TablaCompleta();
            }
        }
        public List<FAC_CAT_IMPUESTO> ObtenCatalogoImpuestos()
        {
            using (var i = new Repositorio<FAC_CAT_IMPUESTO>())
            {
                return i.TablaCompleta();
            }
        }
    }
}
