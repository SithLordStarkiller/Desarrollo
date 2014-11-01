﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAEEEM.Entidades.ModuloCentral
{
    [Serializable]
    public class CampoCustomizableProducto
    {
        public int CvePlantilla { get; set; }
        public int CveCampo { get; set; }
        public int CveProducto { get; set; }
        public string DxNombreCampo { get; set; }
        public string DxDescripcionCampo { get; set; }
        public string DxTipo { get; set; }
        public int? Estatus { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public int? AdicionadoPor { get; set; }
        public int? CveObligatorio { get; set; }
        public int? CveAgregarReporte { get; set; }

        public string ToolTip { get; set; }
        public string Valor { get; set; }

        public List<Valor_Campo_Catalogo> LisValorCatalogos { get; set; }
    }
}
