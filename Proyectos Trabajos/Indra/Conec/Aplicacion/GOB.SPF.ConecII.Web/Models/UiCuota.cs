using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiCuota : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("El clasificador tipo de Servicio*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo servicio es requerido")]
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public List<UiTiposServicio> TipoServicios { get; set; }

        [DisplayName("El clasificador Tipo Referencia*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El clasificador tipo referencia es requerido")]
        public int IdReferencia { get; set; }
        public string Referencia { get; set; }
        public List<UiReferencia> Referencias { get; set; }

        [DisplayName("El clasificador Tipo Dependencia*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El clasificador tipo dependencia es requerido")]
        public int IdDependencia { get; set; }
        public string Dependencia { get; set; }
        public string DescripcionDependencia { get; set; }
        public List<UiDependencias> Dependencias { get; set; }

        [DisplayName("El Concepto*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El clasificador tipo concepto es requerido")]
        [StringLength(1500, ErrorMessage = "El rango máximo es de 1500 caracteres")]
        public string Concepto { get; set; }

        
        [DisplayName("El Jerarquia*")]
        public int IdJerarquia { get; set; }
        public string Jerarquia { get; set; }
        public List<UiJerarquia> Jerarquias { get; set; }

        [DisplayName("El clasificador Tipo Grupo Tarifario*")]
        public int IdGrupoTarifario { get; set; }
        public string GrupoTarifario { get; set; }
        public List<UiGrupoTarifario> GrupoTarifarios { get; set; }
        
        [DisplayName("La Cuota Base*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El clasificador tipo cuota base  es requerido")]
        [Range(1, 999999, ErrorMessage = "La longitud máxima es de 6 y mínima de 1")]
        public decimal CuotaBase { get; set; }

        [DisplayName("El clasificador Medida Cobro*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El clasificador medida cobro  es requerido")]
        public int IdMedidaCobro { get; set; }
        public string MedidaCobro { get; set; }
        public List<UiMedidaCobro> MedidaCobros { get; set; }

        [DisplayName("El clasificador IVA*")]
        [Required(AllowEmptyStrings  = false, ErrorMessage = "El IVA es requerido")]
        [Range(0, 100, ErrorMessage = "La longitud máxima es de 6 y mínima de 1")]
        public decimal Iva { get; set; }

        [DisplayName("La Fecha Autorizacion*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La  fecha autorizacion es requerido")]
        public DateTime FechaAutorizacion { get; set; }

        [DisplayName("La Fecha Entrada Vigor*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha entrada vigor es requerido")]
        public DateTime FechaEntradaVigor { get; set; }

        //[DisplayName("La Fecha Termino*")]
        public DateTime? FechaTermino { get; set; }

        //[DisplayName("La Fecha Publica Dof*")]
        public DateTime? FechaPublicaDof { get; set; }
        public bool IsActive { get; set; }
        public bool? EsProducto { get; set; }
        public string TipoProducto => EsProducto == false ? "Aprobechamiento" : "Producto";
        public int Ano { get; set; }

    }
}