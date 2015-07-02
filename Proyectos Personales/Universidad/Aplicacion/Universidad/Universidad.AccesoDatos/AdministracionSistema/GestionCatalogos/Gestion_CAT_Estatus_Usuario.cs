using System.Collections.Generic;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class Gestion_CAT_Estatus_Usuario
    {
        #region Propiedades de la clase
        /// <summary>
        /// Instancia de la clace Gestion_CAT_Tipos_Usuario
        /// </summary>
        private static readonly Gestion_CAT_Estatus_Usuario _classInstance = new Gestion_CAT_Estatus_Usuario();

        public static Gestion_CAT_Estatus_Usuario ClassInstance
        {
            get { return _classInstance; }
        }

        /// <summary>
        ///  Instancia hacia el contexto de la DB
        /// </summary>
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public Gestion_CAT_Estatus_Usuario()
        {
        }

        #endregion

        #region Motodos Extracion

        /// <summary>
        /// Este metodo se encraga de buscar todos los registro de la tabla US_CAT_NIVEL_USUARIO
        /// </summary>
        /// <returns>regresa una lista de todos los registros de US_CAT_NIVEL_USUARIO</returns>
        public List<US_CAT_ESTATUS_USUARIO> ObtenListaCatEstatusUsuario()
        {
            List<US_CAT_ESTATUS_USUARIO> lista;

            using (var r = new Repositorio<US_CAT_ESTATUS_USUARIO>())
            {
                lista = r.TablaCompleta();
            }

            return lista;
        }

        #endregion

        #region Metodos de Incersion

        /// <summary>
        /// Este metodo almacenara Un registro de la tabla US_CAT_ESTATUS_USUARIO
        /// </summary>
        /// <param name="estatusUsuario">Se enviara el objeto del tipo US_CAT_ESTATUS_USUARIO para añadir su Id_Log_Registro</param>
        /// <param name="AdicionadoPor">Se Enviara el nobre del usuario que inserta el registro</param>
        /// <returns></returns>
        public US_CAT_ESTATUS_USUARIO InsertaCatEstatusUsuario(US_CAT_ESTATUS_USUARIO estatusUsuario)
        {
            using (var r = new Repositorio<US_CAT_ESTATUS_USUARIO>())
            {
                var nueva = r.Agregar(estatusUsuario);

                return nueva;
            }
        }

        #endregion
    }
}
