using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
  public class Solicitante 
  {
    public int Identificador { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
    public bool Activo { get; set; }


//        [IdSolicitante]
//        [int] IDENTITY(1,1) NOT NULL,

//[RazonSocial] [varchar](300) NULL,
//	[NombreCorto]
//        [varchar](100) NULL,
//	[RFC]
//        [varchar](13) NULL,
//	[ApellidoPaterno]
//        [varchar](100) NULL,
//	[ApellidoMaterno]
//        [varchar](100) NULL,
//	[Nombres]
//        [varchar](200) NULL,
//	[CargoSolicitante]
//        [varchar](100) NULL,
//	[Telefono]
//        [varchar](50) NULL,
//	[FechaInicial]
//        [date]
//        NOT NULL,

//    [FechaFinal] [date]
//        NULL,
//	[Activo]
//        [bit]
//        NOT NULL,
  }
}
