using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitudServicioFormulario
    {
        #region TIPO SERVICIO
        public int IdTipoServicio { get; set; }
        public string NombreTipoServicio { get; set; }
        #endregion

        #region CAPACITACIÓN
        public string NombreCurso { get; set; }
        public int NumeroParticipantes { get; set; }
        public int horas { get; set; }
        public int NumeroPeriodo { get; set; }
        public int IdTipoPeriodo { get; set; }
        public int IdTipoInstalacion { get; set; }
        #endregion

        #region CERTIFICACIÓN
        public int IdEstandarCompetencia { get; set; }
        public DateTime FechaExamen { get; set; }
        #endregion CERTIFICACIÓN
        
        #region INTRAMUROS
        public int IdConcepto { get; set; }
        #endregion

        #region TECNOLOGÍAS
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        #endregion

        #region SEGURIDAD - PROTECCIÓN A PERSONAS
        //se completa con las variables anteriormente declaradas
        #endregion
        #region SEGURIDAD - CUSTODIA DE BIENES
        public int IdTipoBienCustodia { get; set; }
        #endregion

        #region TODOS
        public string DocumentoSoporte { get; set; }
        public string Observacion { get; set; }
        public bool Comite { get; set; }
        public string Comentario { get; set; }
        #endregion
    }
}