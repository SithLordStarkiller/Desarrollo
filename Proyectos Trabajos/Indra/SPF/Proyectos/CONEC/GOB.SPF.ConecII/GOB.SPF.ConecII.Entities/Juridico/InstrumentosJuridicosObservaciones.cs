using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.Genericos;

namespace GOB.SPF.ConecII.Entities.Juridico
{
   public class InstrumentosJuridicosObservaciones : TEntity
    {
        public int? IdInstrumentoJuridicoObservacionPadre { get; set; }
        public InstrumentosJuridicos InstrumentoJuridico { get; set; }
        public int IdUsuario { get; set; }
        public Estatus Estatus { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaObservacion { get; set; }
        public TiposObservacion TipoObservacion { get; set; }
    }
}
