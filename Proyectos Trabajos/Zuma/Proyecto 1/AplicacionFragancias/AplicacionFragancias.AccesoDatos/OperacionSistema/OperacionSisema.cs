using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.OperacionSistema
{
    public class OperacionSisema
    {
        public List<SIS_MENUARBOL> ObtenListaMenuArbol()
        {
            using (var r = new Repositorio<SIS_MENUARBOL>())
            {
                return r.TablaCompleta();
            }
        }
    }
}
