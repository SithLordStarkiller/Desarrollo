using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class Gestion_CAT_Nivel_Usuario
    {
        #region Propiedades de la clase
        /// <summary>
        /// Instancia de la clace Gestion_CAT_Tipos_Usuario
        /// </summary>
        private static readonly Gestion_CAT_Nivel_Usuario _classInstance = new Gestion_CAT_Nivel_Usuario();

        public static Gestion_CAT_Nivel_Usuario ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public Gestion_CAT_Nivel_Usuario()
        {
        }

        #endregion

        #region Motodos Extracion

        /// <summary>
        /// Este metodo se encraga de buscar todos los registro de la tabla US_CAT_NIVEL_USUARIO
        /// </summary>
        /// <returns>regresa una lista de todos los registros de US_CAT_NIVEL_USUARIO</returns>
        public List<US_CAT_NIVEL_USUARIO> ObtenListaCatNivelUsuario()
        {
            List<US_CAT_NIVEL_USUARIO> Lista = null;

            using (var r = new Repositorio<US_CAT_NIVEL_USUARIO>())
            {
                Lista = r.TablaCompleta();
            }

            return Lista;
        }

        #endregion

        #region Metodos de Insercion

        /// <summary>
        /// Este metodo almacenara Un registro de la tabla US_CAT_NIVEL_USUARIO
        /// </summary>
        /// <param name="EstatusUsuario">Se enviara el objeto del tipo US_CAT_NIVEL_USUARIO para añadir su Id_Log_Registro</param>
        /// <returns></returns>
        public US_CAT_NIVEL_USUARIO InsertaCatEstatusUsuario(US_CAT_NIVEL_USUARIO nivelUsuario)
        {
            using (var r = new Repositorio<US_CAT_NIVEL_USUARIO>())
            {
                var Nueva = r.Agregar(nivelUsuario);

                return Nueva;
            }
        }

        #endregion
    }
}
