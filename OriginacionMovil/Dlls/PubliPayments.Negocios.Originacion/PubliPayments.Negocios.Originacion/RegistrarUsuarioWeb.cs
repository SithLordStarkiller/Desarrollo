using System.Collections.Generic;
using OriginacionMovil.Models;
using PubliPayments.Entidades.Originacion;

namespace PubliPayments.Negocios.Originacion
{
    public class RegistrarUsuarioWeb
    {
        public List<CatEmpresa> ObtenerEmpresas()
        {
            var res = new EntCatEmpresa().ObtenerEmpresas();

            return res;
        }

        public List<CatLugar> ObtenerLugares()
        {
            var res = new EntCatLugar().ObtenerLugares();

            return res;
        }

        public List<CatEstados> ObtenerEstados()
        {
            return new EntCatEstado().ObtenerEstados();
        }
    }
}
