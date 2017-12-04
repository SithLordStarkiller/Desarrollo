using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class InstrumentoJuridicoOficioCarta : TEntity
    {
        public InstrumentosJuridicosDetalle InstrumentoJuridicoDetalle { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public TiposOficioDistribucion TipoOficioDistribucion { get; set; }
        public Articulos Articulo { get; set; }
        public Anio AnioFiscal { get; set; }
        public string FolioOficio { get; set; }
        public int? NumeroConsecutivo { get; set; }
        public string NumeroEjemplar { get; set; }
        public string FolioOficioResponder { get; set; }
        public DateTime FechaCartaOficio { get; set; }
        public DateTime? FechaContinuacionServicio { get; set; }
        public DateTime? FechaTerminoServicio { get; set; }
        public DateTime? FechaOficioResponder { get; set; }
        public List<InstrumentosJuridicosIntegrantes> InstrumentosJuridicosIntegrantes { get; set; }
        public List<InstrumentosJuridicosContratantes> InstrumentosJuridicosContratantes { get; set; }
    }
}
