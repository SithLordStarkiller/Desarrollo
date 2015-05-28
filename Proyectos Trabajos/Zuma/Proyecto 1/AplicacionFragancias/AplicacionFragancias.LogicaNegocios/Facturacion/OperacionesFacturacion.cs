using System.Collections.Generic;
using AplicacionFragancias.AccesoDatos.Facturacion;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.LogicaNegocios.Facturacion
{
    public class OperacionesFacturacion
    {
        public List<FAC_CAT_CONDICIONES_PAGO> ObtenCatalogoTipoPago()
        {
            return new OperacionesCatalogosFacturacion().ObtenCatalogoTipoPago();
        }

        public List<FAC_CAT_IMPUESTO> ObtenCatalogoImpuestos()
        {
            return new OperacionesCatalogosFacturacion().ObtenCatalogoImpuestos();
        }

        public List<FAC_CAT_MONEDA> ObtenCatalogosMonedas()
        {
            return new OperacionesCatalogosFacturacion().ObtenCatalogoMoneda();
        }
    }
}
