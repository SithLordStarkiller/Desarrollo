using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;
using Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos;

namespace Universidad.LogicaNegocios.GestionCatalogos
{
    public class GestionCatalogos
    {
        #region Propiedades de la clase
        private static readonly GestionCatalogos _ClassInstance = new GestionCatalogos();

        public static GestionCatalogos ClassInstance
        {
            get { return _ClassInstance; }
        }

        public GestionCatalogos()
        {
        }

        #endregion

        #region Metodos de Insercion
        public US_CAT_TIPO_USUARIO InsertaRegistroCatTipoUsuario(US_CAT_TIPO_USUARIO Registro)
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.InsertaRegistro(Registro);
        }
        #endregion

        #region Metodos de Extraccion

        public List<US_CAT_TIPO_USUARIO> ObtenListaCatTiposUsuario()
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.ObtenListaCatTiposUsuario();
        }

        public US_CAT_TIPO_USUARIO ObtenTipoUsuario(int Id_Tipo_Usuario)
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.ObtenCatTipoUsuario(Id_Tipo_Usuario);
        }

        public List<PER_CAT_NACIONALIDAD> ObtenNacionalidades()
        {
            return new GestionCatNacionalidadA().ObtenNacionalidadLinq();
        }

        #endregion
    }
}
