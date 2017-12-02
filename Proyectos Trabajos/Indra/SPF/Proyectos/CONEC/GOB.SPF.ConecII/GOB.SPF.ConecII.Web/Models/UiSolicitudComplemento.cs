using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitudComplemento: UiEntity
    {
        [Key]
        [DisplayName(@"No. Solicitud")]
        public int IdSolicitud { get; set; }

        [DisplayName(@"Razón Social")]
        public string RazonSocial { get; set; }

        [DisplayName(@"RFC*")]
        public string RFC { get; set; }

        [DisplayName(@"Tipo de Servicio")]
        public string TipoServicio { get; set; }

        [DisplayName(@"No. Instalaciones")]
        public int NumInstalaciones { get; set; }

        [DisplayName(@"Estatus")]
        public bool Status { get; set; }

        [DisplayName(@"Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicio { get; set; }

        [DisplayName(@"Fecha Fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFin { get; set; }

        public int TipoRolUsuario { get; set; }

        [DisplayName(@"Tipo Instalaciones")]
        public string TipoInstalaciones { get; set; }

        [DisplayName(@"Horas duración")]
        public int HorasDuracion { get; set; }

        [DisplayName(@"Concepto")]
        public int IdConcepto { get; set; }

        [DisplayName(@"Número de Células")]
        public int NumCelulas { get; set; }

        [DisplayName(@"Descripción")]
        public string DescripcionConcepto { get; set; }

        public List<int> IdGasto { get; set; }

        [DisplayName(@"Gastos Inherentes")]
        public bool CapturaGastosInherentes { get; set; }

        public List<SolicitudInstalacion> Instalaciones { get; set; }

        public List<DocumentoServicio> DocumentosServicio { get; set; }

        public List<FactorPorInstalacion> FactoresPorInstalacion { get; set; }

        public List<Signatario> Signatarios { get; set; }

        public List<Concepto> Conceptos { set; get; }

        public List<Gasto> Gastos { get; set; }

    }

    public class KeyDescripcion {
        public string Key { get; set; }
        public string Descripcion { get; set; }
    }

    public class Concepto : KeyDescripcion
    {

    }

    public class Gasto : KeyDescripcion
    {

    }



    public class FactorPorInstalacion {
        public int IdInstalaccion { get; set; }

        [DisplayName(@"Conceptos")]
        public string Nombre { get; set; }

        [DisplayName(@"División")]
        public string Division { get; set; }

        [DisplayName(@"Grupo")]
        public string Grupo { get; set; }

        [DisplayName(@"Fracción")]
        public string Fraccion { get; set; }

        [DisplayName(@"Actividad")]
        public string Actividad { get; set; }

        [DisplayName(@"Distancia")]
        public string Distancia { get; set; }

        [DisplayName(@"Criminalidad")]
        public string Criminalidad { get; set; }

        public List<EstadoFuerza> EstadosFuerza { get; set; }

        public List<Suministro> Suministros { get; set; }
    }

    public class Suministro {

        [DisplayName(@"Concepto")]
        public string Concepto { get; set; }

        [DisplayName(@"Turnos")]
        public string Turnos { get; set; }

        [DisplayName(@"Cantidad")]
        public int Cantidad { get; set; }

        [DisplayName(@"M")]
        public int Masculinos { get; set; }

        [DisplayName(@"F")]
        public int Femeninos { get; set; }

        [DisplayName(@"I")]
        public int Indistintos { get; set; }

        [DisplayName(@"AL")]
        public int ArmaLarga { get; set; }

        [DisplayName(@"AC")]
        public int ArmaCorta { get; set; }

        [DisplayName(@"M")]
        public int Municiones { get; set; }

        [DisplayName(@"U")]
        public int VestuarioUniforme { get; set; }

        [DisplayName(@"VG")]
        public int VestuarioGala { get; set; }

        [DisplayName(@"VMG")]
        public int VestuarioMediaGala { get; set; }

        [DisplayName(@"T")]
        public int EquTactico { get; set; }

        [DisplayName(@"A")]
        public int EquAntimotin { get; set; }

        [DisplayName(@"TS")]
        public int EquTaser { get; set; }

        [DisplayName(@"R")]
        public int TelcoRadio { get; set; }

        [DisplayName(@"A")]
        public int TelcoAntena { get; set; }
    }

    public class EstadoFuerza {

        [DisplayName(@"Conceptos")]
        public string Concepto { get; set; }

        [DisplayName(@"Descripción")]
        public string Descripcion { get; set; }

        [DisplayName(@"Turnos")]
        public string Turnos { get; set; }

        [DisplayName(@"Cantidad")]
        public int Cantidad { get; set; }

        [DisplayName(@"L")]
        public int Lunes { get; set; }

        [DisplayName(@"M")]
        public int Martes { get; set; }

        [DisplayName(@"M")]
        public int Miercoles { get; set; }

        [DisplayName(@"J")]
        public int Jueves { get; set; }

        [DisplayName(@"V")]
        public int Viernes { get; set; }

        [DisplayName(@"S")]
        public int Sabado { get; set; }

        [DisplayName(@"D")]
        public int Domingo { get; set; }

    }

    public class Signatario {

        public int IdSignatario { get; set; }

        [DisplayName(@"Nombre Completo")]
        public string NombreCompleto { get; set; }

        [DisplayName(@"Área Funcional")]
        public string AreaFuncional { get; set; }

        [DisplayName(@"Cargo")]
        public string Cargo { get; set; }

        public bool EsActivo { get; set; }
    }

    public class DocumentoServicio {
        public int IdDocumentoServicio { get; set; }

        [DisplayName(@"Tipo Documento")]
        public string TipoDocumento { get; set; }

        [DisplayName(@"Versión")]
        public string Version { get; set; }

        [DisplayName(@"Fecha Envío Validación")]
        public DateTime FechaEnvioValidacion { get; set; }

        [DisplayName(@"Observaciones")]
        public string Observaciones { get; set; }

        [DisplayName(@"Fecha Observaciones")]
        public string FechaObservaciones { get; set; }

        public bool EsActivo { get; set; }
    }

    public class DocumentosServicio {
        public int IdDocumento { get; set; }
        public int idTipoDocumento { get; set; }
        public int Version { get; set; }
        public DateTime FechaEnvioValidacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaObservaciones { get; set; }
    }

    public class SolicitudInstalacion {

        [Key]
        public int IdSolicitudInstalacion { get; set; }

        [DisplayName(@"No.")]
        public int IdInstalacion { get; set; }

        [DisplayName(@"Nombre")]
        public string Nombre { get; set; }

        [DisplayName(@"Dirección")]
        public string Direccion { get; set; }

        public bool Seleccionado { get; set; }

        [DisplayName(@"Concepto")]
        public int IdConcepto { get; set; }

        [DisplayName(@"Número de Células")]
        public int NumCelulas { get; set; }

        [DisplayName(@"Descripción")]
        public string DescripcionConcepto { get; set; }
    }

}