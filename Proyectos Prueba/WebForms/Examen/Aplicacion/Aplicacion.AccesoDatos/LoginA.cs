using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Entidades;

namespace Aplicacion.AccesoDatos
{
    public class LoginA
    {
        private readonly ExamenEntities _contexto = new ExamenEntities();

        public PER_PERSONAS LoginPersonas(string correoElectronico, string contrasena)
        {
            PER_PERSONAS detalle;

            using (var r = new Repositorio<PER_PERSONAS>())
            {
                detalle = r.Extraer(me => me.CORREOELECTRONICO == correoElectronico && me.CONTRASENA == contrasena);
            }

            return detalle;
        }

        public List<CAT_AREANEGOCIO> ListaAreaNegocios()
        {
            using (var r = new Repositorio<CAT_AREANEGOCIO>())
            {
                return  r.TablaCompleta();
            }
            
        }
    }
}
