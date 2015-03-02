using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.AccesoDatos;

namespace AplicacionFragancias.LogicaNegocios.OperacionSistema
{
    public class OperaionSistema
    {
        public List<SIS_MENUARBOL> ObtenListaMenuArbol()
        {
            return new AccesoDatos.OperacionSistema.OperacionSisema().ObtenListaMenuArbol();
        }
    }
}
