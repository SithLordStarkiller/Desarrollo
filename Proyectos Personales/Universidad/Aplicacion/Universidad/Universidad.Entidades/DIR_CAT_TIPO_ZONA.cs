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
public partial class DIR_CAT_TIPO_ZONA

{
    public DIR_CAT_TIPO_ZONA()
    {
        this.DIR_CAT_COLONIAS = new HashSet<DIR_CAT_COLONIAS>();
    }


	[DataMember]
    public int IDTIPOZONA { get; set; }


	[DataMember]
    public string TIPOZONA { get; set; }


    public virtual ICollection<DIR_CAT_COLONIAS> DIR_CAT_COLONIAS { get; set; }
}
