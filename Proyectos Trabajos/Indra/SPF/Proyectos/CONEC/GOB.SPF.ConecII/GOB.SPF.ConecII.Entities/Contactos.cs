using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
  public class Contactos
    {
    public int Identificador { get; set; }
    public int IdSolicitante { get; set; }
    public int IdTipoContacto { get; set; }
    public string Nombre { get; set; }
    public string CargoContacto { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string CP { get; set; }
    public int IdEntidadFederativa { get; set; }
    public int IdMunicipio { get; set; }
    public string Observaciones { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
    public bool Activo { get; set; }
  }
}
