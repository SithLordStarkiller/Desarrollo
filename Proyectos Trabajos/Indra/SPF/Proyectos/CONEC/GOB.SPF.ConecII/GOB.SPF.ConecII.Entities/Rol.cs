using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Rol : IRol
    {
        #region constructores
        public Rol(int id)
        {
            Id = id;
        }

        public Rol()
        {
            
        }
        #endregion

        #region variables publicas
        public int Id { get; set; }
        public int? IdParentRol { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public int? IdArea { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
        #endregion
    }
}
