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
public partial class AUL_AULA_CLASES

{
    public AUL_AULA_CLASES()
    {
        this.MAT_HORARIO_POR_MATERIA = new HashSet<MAT_HORARIO_POR_MATERIA>();
    }


	[DataMember]
    public short IDAULACLASES { get; set; }


	[DataMember]
    public Nullable<short> IDTIPOAULA { get; set; }


	[DataMember]
    public string AULA { get; set; }


	[DataMember]
    public Nullable<short> MAXLUGARES { get; set; }


    public virtual AUL_CAT_TIPO_AULA AUL_CAT_TIPO_AULA { get; set; }
    public virtual ICollection<MAT_HORARIO_POR_MATERIA> MAT_HORARIO_POR_MATERIA { get; set; }
}
