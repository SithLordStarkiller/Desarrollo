using System.Collections.Generic;
using System.ServiceModel;
using Universidad.Entidades;
using Universidad.Entidades.Catalogos;

namespace Universidad.ServidorInterno.GestionCatalogos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_GestionCatalogos" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_GestionCatalogos
    {
        [OperationContract]
        List<US_CAT_TIPO_USUARIO> ObtenTablaUsCatTipoUsuarios();

        [OperationContract]
        List<US_CAT_NIVEL_USUARIO> ObtenTablaUsCatNivelUsuario();

        [OperationContract]
        List<US_CAT_ESTATUS_USUARIO> ObtenTablaUsCatEstatusUsuario();

        [OperationContract]
        US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int idTipoUsuario);

        [OperationContract]
        List<PER_CAT_NACIONALIDAD> ObtenCatalogoNacionalidades();

        [OperationContract]
        List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona();

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal);

        [OperationContract]
        List<DIR_CAT_ESTADO> ObtenCatEstados();

        [OperationContract]
        List<DIR_CAT_DELG_MUNICIPIO> ObtenMunicipios(int estado);

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio);

        [OperationContract]
        DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia);

        [OperationContract]
        List<ListasGenerica> ObtenTablasCatalogos();

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenCatalogosColonias();

        [OperationContract]
        List<DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios();
    }
}
