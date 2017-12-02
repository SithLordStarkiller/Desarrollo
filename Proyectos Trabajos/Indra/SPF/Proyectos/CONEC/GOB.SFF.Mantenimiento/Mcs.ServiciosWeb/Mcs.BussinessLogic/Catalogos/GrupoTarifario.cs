using System;
using System.Collections.Generic;
using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.Core.Log4Net;
using Mcs.DataAccess.Repositories;

namespace Mcs.BussinessLogic.Catalogos
{
    /// <summary>
    /// Clase Negocio para obtener el Grupo Tarifario de la Base de datos de "COVE".
    /// </summary>
    public class GrupoTarifario
    {
        //private IConnectionFactory connectionFactory;
        /// <summary>
        /// Obtiene el catálogo de Grupo Tarifario.
        /// </summary>
        /// <returns>Devuelve un objeto response que contiene la lista de entidades del grupo tarifario.</returns>
        public ResponseDto<List<BeGrupoTarifario>> GetGrupoTarifario()
        {

            ResponseDto<List<BeGrupoTarifario>> result;

            //clsEntSesion objSession = new clsEntSesion
            //{
            //    usuario = new clsEntUsuario
            //    {
            //        UsuLogin = "dell-482",
            //        UsuContrasenia = Encriptacion.Encrypt("12345678")
            //    },
            //    inicio = DateTime.Now,
            //    estatus = clsEntSesion.tipoEstatus.Activa
            //};

            try
            {
                

                //var dataResult = clsDatCatalogos.consultaCatalogo("catalogo.spuConsultarGrupoTarifario", objSession, "bdcove");

                //var listDataResult = dataResult.CreateListFromTable<BeGrupoTarifario>();

                

                var listDataResult = new GrupoTarifarioRepository(null).GetGrupoTarifarios();

                //if (listDataResult == null) throw new Exception("Ocurrió un error al obte");
                result = new ResponseDto<List<BeGrupoTarifario>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeGrupoTarifario>)listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "Cove", "Grupo Tarifario", ex);
                result = new ResponseDto<List<BeGrupoTarifario>>
                {
                    Success = false,
                    Value = null,
                    Message = ex.Message
                };
            }


            return result;
        }
    }
}
