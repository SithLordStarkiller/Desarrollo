//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


[DataContract]
public partial class SIS_WADM_ARBOLMENU

{
    public SIS_WADM_ARBOLMENU()
    {
        this.SIS_WADM_ARBOLMENU1 = new HashSet<SIS_WADM_ARBOLMENU>();
    }


	[DataMember]
    public int IDMENU { get; set; }


	[DataMember]
    public Nullable<int> ID_TIPO_USUARIO { get; set; }


	[DataMember]
    public Nullable<int> ID_NIVEL_USUARIO { get; set; }


	[DataMember]
    public Nullable<int> IDMENUPADRE { get; set; }


	[DataMember]
    public string NOMBRE { get; set; }


	[DataMember]
    public string LINK { get; set; }


    public virtual ICollection<SIS_WADM_ARBOLMENU> SIS_WADM_ARBOLMENU1 { get; set; }
    public virtual SIS_WADM_ARBOLMENU SIS_WADM_ARBOLMENU2 { get; set; }
    public virtual US_CAT_NIVEL_USUARIO US_CAT_NIVEL_USUARIO { get; set; }
    public virtual US_CAT_TIPO_USUARIO US_CAT_TIPO_USUARIO { get; set; }
}
