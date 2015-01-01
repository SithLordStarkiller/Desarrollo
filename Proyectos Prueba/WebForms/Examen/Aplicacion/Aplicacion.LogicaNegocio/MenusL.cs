using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Entidades;
using Aplicacion.AccesoDatos;

namespace Aplicacion.LogicaNegocio
{
    public class MenusL
    {
        public List<SIS_APLICACIONNES> ListaAplicacionness()
        {
            var accesoDatos = new MenusA();
            return accesoDatos.ListaAplicacionnes();
        }
    }
}
