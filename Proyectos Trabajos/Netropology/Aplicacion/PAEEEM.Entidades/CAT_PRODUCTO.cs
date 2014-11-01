//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_PRODUCTO
    {
        public CAT_PRODUCTO()
        {
            this.K_CREDITO_PRODUCTO = new HashSet<K_CREDITO_PRODUCTO>();
            this.K_PROVEEDOR_PRODUCTO = new HashSet<K_PROVEEDOR_PRODUCTO>();
        }
    
        public int Cve_Producto { get; set; }
        public int Cve_Tecnologia { get; set; }
        public int Cve_Fabricante { get; set; }
        public int Cve_Marca { get; set; }
        public int Cve_Estatus_Producto { get; set; }
        public string Dx_Nombre_Producto { get; set; }
        public string Dx_Modelo_Producto { get; set; }
        public Nullable<decimal> Mt_Precio_Max { get; set; }
        public Nullable<double> No_Eficiencia_Energia { get; set; }
        public Nullable<double> No_Max_Consumo_24h { get; set; }
        public Nullable<double> No_Capacidad { get; set; }
        public System.DateTime Dt_Fecha_Producto { get; set; }
        public Nullable<int> Ft_Tipo_Producto { get; set; }
        public string Dx_Producto_Code { get; set; }
        public Nullable<int> Cve_Capacidad_Sust { get; set; }
        public Nullable<double> Ahorro_Consumo { get; set; }
        public Nullable<double> Ahorro_Demanda { get; set; }
        public Nullable<int> Cve_Tipo_SE { get; set; }
        public Nullable<int> Cve_Transformador_SE { get; set; }
        public Nullable<int> Cve_Fase_Transformador { get; set; }
        public Nullable<int> Cve_Marca_Transform { get; set; }
        public Nullable<int> Cve_Relacion_Transform { get; set; }
        public Nullable<int> Cve_Apartarrayos_SE { get; set; }
        public Nullable<int> Cve_Marca_Apartar { get; set; }
        public Nullable<int> Cve_Cortacircuito_SE { get; set; }
        public Nullable<int> Cve_Marca_Cortacirc { get; set; }
        public Nullable<int> Cve_Interruptor_SE { get; set; }
        public Nullable<int> Cve_Marca_Interrup { get; set; }
        public Nullable<int> Cve_Conductor_SE { get; set; }
        public Nullable<int> Cve_Marca_Conductor { get; set; }
        public Nullable<int> Cve_Cond_Conex_SE { get; set; }
        public Nullable<int> Cve_Cond_Conex_Mca { get; set; }
        public Nullable<double> Mt_Precio_Unitario { get; set; }
        public string Cve_Unidad { get; set; }
        public Nullable<byte> IdSistemaArreglo { get; set; }
        public Nullable<int> Cve_Potencia { get; set; }
        public Nullable<byte> Cve_Tipo { get; set; }
        public string Dx_Tipo_Dielectrico { get; set; }
        public Nullable<int> Cve_Proteccion_Interna { get; set; }
        public Nullable<byte> Cve_Proteccion_Externa { get; set; }
        public Nullable<byte> Cve_Material { get; set; }
        public Nullable<byte> Cve_Perdidas { get; set; }
        public Nullable<byte> Cve_Proteccion { get; set; }
        public Nullable<byte> Cve_Prot_Contra_Fuego { get; set; }
        public Nullable<byte> Cve_Anclaje { get; set; }
        public Nullable<byte> Cve_Terminal_Tierra { get; set; }
        public Nullable<int> Cve_Tipo_Controlador { get; set; }
        public Nullable<int> Cve_P_Corriente_Controlador { get; set; }
        public Nullable<int> Cve_P_Sobre_Temperatura { get; set; }
        public Nullable<int> Cve_P_Sobre_Distorsion_V { get; set; }
        public Nullable<int> Cve_Bloqueo_Prog_Display { get; set; }
        public Nullable<byte> Cve_Tipo_Comunicacion { get; set; }
        public Nullable<byte> Cve_Proteccion_Gabinete { get; set; }
        public Nullable<int> Cve_Clase_SE { get; set; }
        public Nullable<decimal> Precio_Total_SE { get; set; }
        public Nullable<byte> Cve_Protec_Contra_Exp { get; set; }
    
        public virtual CAT_CAPACIDAD_SUSTITUCION CAT_CAPACIDAD_SUSTITUCION { get; set; }
        public virtual CAT_ESTATUS_PRODUCTO CAT_ESTATUS_PRODUCTO { get; set; }
        public virtual CAT_FABRICANTE CAT_FABRICANTE { get; set; }
        public virtual CAT_MARCA CAT_MARCA { get; set; }
        public virtual CAT_MODULOS_SE CAT_MODULOS_SE { get; set; }
        public virtual CAT_SE_COND_CONEXION_MARCA CAT_SE_COND_CONEXION_MARCA { get; set; }
        public virtual CAT_SE_CORTACIRC_MARCA CAT_SE_CORTACIRC_MARCA { get; set; }
        public virtual CAT_SE_INTERRUPTOR_MARCA CAT_SE_INTERRUPTOR_MARCA { get; set; }
        public virtual CAT_TIPO_PRODUCTO CAT_TIPO_PRODUCTO { get; set; }
        public virtual CAT_SE_APARTARRAYO CAT_SE_APARTARRAYO { get; set; }
        public virtual CAT_SE_COND_CONEXION CAT_SE_COND_CONEXION { get; set; }
        public virtual CAT_SE_CONDUCTOR_MARCA CAT_SE_CONDUCTOR_MARCA { get; set; }
        public virtual CAT_SE_CONDUCTOR CAT_SE_CONDUCTOR { get; set; }
        public virtual CAT_SE_CORTACIRCUITO CAT_SE_CORTACIRCUITO { get; set; }
        public virtual CAT_SE_INTERRUPTOR CAT_SE_INTERRUPTOR { get; set; }
        public virtual CAT_SE_TIPO CAT_SE_TIPO { get; set; }
        public virtual CAT_SE_TRANSFORM_FASE CAT_SE_TRANSFORM_FASE { get; set; }
        public virtual CAT_SE_TRANSFORMADOR CAT_SE_TRANSFORMADOR { get; set; }
        public virtual CAT_TECNOLOGIA CAT_TECNOLOGIA { get; set; }
        public virtual CAT_SE_TRANSFORM_MARCA CAT_SE_TRANSFORM_MARCA { get; set; }
        public virtual CAT_SE_TRANSFORM_RELACION CAT_SE_TRANSFORM_RELACION { get; set; }
        public virtual CAT_SE_APARTARRAYO_MARCA CAT_SE_APARTARRAYO_MARCA { get; set; }
        public virtual ICollection<K_CREDITO_PRODUCTO> K_CREDITO_PRODUCTO { get; set; }
        public virtual ICollection<K_PROVEEDOR_PRODUCTO> K_PROVEEDOR_PRODUCTO { get; set; }
    }
}
