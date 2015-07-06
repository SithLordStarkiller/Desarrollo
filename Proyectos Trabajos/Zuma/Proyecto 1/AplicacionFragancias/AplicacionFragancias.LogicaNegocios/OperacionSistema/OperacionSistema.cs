using System.Collections.Generic;
using AplicacionFragancias.AccesoDatos.OperacionSistema;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.LogicaNegocios.OperacionSistema
{
    public class OperacionSistema
    {
        public US_USUARIOS ObtenUsuario(string usuario, string contrasena)
        {
            return new OperacionSisema().ObtenUsuario(usuario, contrasena);
        }

        public List<SIS_MENUARBOL> ObtenListaMenuArbol(US_USUARIOS usuario)
        {
            return new OperacionSisema().ObtenListaMenuArbol(usuario);
        }
    }
}
