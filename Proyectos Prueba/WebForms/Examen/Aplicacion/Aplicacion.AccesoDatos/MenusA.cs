using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Entidades;

namespace Aplicacion.AccesoDatos
{
    public class MenusA
    {
        private readonly ExamenEntities _contexto = new ExamenEntities();

        public List<SIS_APLICACIONNES> ListaAplicacionnes()
        {
            using (var r = new Repositorio<SIS_APLICACIONNES>())
            {
                return  r.TablaCompleta();
            }
        }
    }
}
