using System.Collections.Generic;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Compras
{
    public class OperacionesCatalogosCompras
    {
        public List<COM_CAT_ESTATUS_COMPRA> ObtenEstatusCompras()
        {
            using (var i = new Repositorio<COM_CAT_ESTATUS_COMPRA>())
            {
                return i.TablaCompleta();
            }
        }

        public List<COM_CAT_UNIDADES_MEDIDA> ObtenUnidadesMedida()
        {
            using (var r = new Repositorio<COM_CAT_UNIDADES_MEDIDA>())
            {
                return r.TablaCompleta();
            }
        }

        public List<COM_CAT_PRESENTACION> ObtenPresentacion()
        {
            using (var r = new Repositorio<COM_CAT_PRESENTACION>())
            {
                return r.TablaCompleta();
            }
        }
    }
}