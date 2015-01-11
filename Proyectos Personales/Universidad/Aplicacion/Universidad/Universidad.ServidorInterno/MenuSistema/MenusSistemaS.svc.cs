using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Universidad.Entidades;
using Universidad.LogicaNegocios.MenuSistema;

namespace Universidad.ServidorInterno.MenuSistema
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenusSistemaS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MenusSistemaS.svc or MenusSistemaS.svc.cs at the Solution Explorer and start debugging.
    public class MenusSistemaS : IMenusSistemaS
    {
        public string TraeArbol()
        {
            var lista = new MenuSistemaAdministrativoL().TraeArbol();
            return JsonConvert.SerializeObject(lista);
        }

        public string TraerMenus(US_USUARIOS usuario)
        {
            var lista = new MenuSistemaAdministrativoL().TraeMenus(usuario);
            return JsonConvert.SerializeObject(lista);
        }
    }
}
