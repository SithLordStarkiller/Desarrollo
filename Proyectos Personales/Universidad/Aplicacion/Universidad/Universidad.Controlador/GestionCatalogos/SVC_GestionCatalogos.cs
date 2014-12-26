using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Universidad.Controlador;
using Universidad.Controlador.SRV_GestionCatalogos;
using Universidad.Entidades;
using System.ServiceModel;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.GestionCatalogos
{
    public class SVC_GestionCatalogos
    {
        #region Propiedades de la clase

        private S_GestionCatalogosClient S_GestionCatalogos = null;

        public SVC_GestionCatalogos(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            S_GestionCatalogos = new S_GestionCatalogosClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"GestionCatalogos/", "S_GestionCatalogos.svc"));
        }

        public System.EventHandler Logeo_Finalizado = null;

        #endregion

        /// <summary>
        /// Este metodo Obtine una lista completa con todos los registros de la tabla US_CAT_TIPO_USUARIO del servidor interno 
        /// </summary>
        /// <returns>Regresa una lista con todos los registros de la tabla US_CAT_TIPO_USUARIO</returns>
        public List<US_CAT_TIPO_USUARIO> ObtenCatNivelUsuarios()
        {
            var JLista = S_GestionCatalogos.ObtenTablaUsCatTipoUsuarios();

            var listaCatNivelUsuarios =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<US_CAT_TIPO_USUARIO>>(JLista);
            return listaCatNivelUsuarios;
        }

        public US_CAT_TIPO_USUARIO ObtenTipoUsuario(int Id_TipoUsuario)
        {
            var JObject = S_GestionCatalogos.ObtenCatTipoUsuario(Id_TipoUsuario);

            var TipoUsuario = JsonConvert.DeserializeObject<US_CAT_TIPO_USUARIO>(JObject);
            
            return TipoUsuario;
        }
    }
}
