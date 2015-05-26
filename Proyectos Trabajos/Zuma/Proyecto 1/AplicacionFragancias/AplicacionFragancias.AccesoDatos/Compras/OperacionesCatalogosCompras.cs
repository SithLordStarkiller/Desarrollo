using System;
using System.Collections.Generic;
using System.Linq;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Compras
{
    public class OperacionesCatalogosCompras
    {
        public List<COM_ESTATUS_COMPRA> ObtenEstatusCompras()
        {
            using (var i = new Repositorio<COM_ESTATUS_COMPRA>())
            {
                return i.TablaCompleta();
            }
        }
    }

}