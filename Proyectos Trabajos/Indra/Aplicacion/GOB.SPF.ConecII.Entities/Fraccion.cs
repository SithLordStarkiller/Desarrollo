﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Fraccion : Request.RequestBase
    {
        #region Propiedades
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGrupo { get; set; }
        public int IdDivision { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        #endregion Propiedades

        #region PropiedadesAdiciones
        public string Division { get; set; }
        public string Grupo { get; set; }
        #endregion PropiedadesAdiciones
    }
}