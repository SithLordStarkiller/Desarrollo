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
public partial class CAL_CALIFICACIONES_FECHAS

{
    public CAL_CALIFICACIONES_FECHAS()
    {
        this.CAL_CALIFICACIONES = new HashSet<CAL_CALIFICACIONES>();
    }


	[DataMember]
    public int IDCALIFICACIONESFECHAS { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONPRIMERPERIODOORDINARIO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONSEGUNDOPERIODOORDINARIO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONTERCERPERIODOORDINARIO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONFINALORDINARIA { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONPRIMERPERIODORECURSAMIENTO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONSEGUNDOPERIODORECURSAMIENTO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONTERCERPERIODORECURSAMIENTO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONFINALRECURSAMIENTO { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONETS1 { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONETS2 { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONETS3 { get; set; }


	[DataMember]
    public Nullable<System.DateTime> FECHACALIFICACIONETS4 { get; set; }


    public virtual ICollection<CAL_CALIFICACIONES> CAL_CALIFICACIONES { get; set; }
}