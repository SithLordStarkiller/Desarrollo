using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcs.Core.Common;
using Mcs.Core.Extension;


using Mcs.Core.Log4Net;
using Mcs.Core.Security;
using Mcs.DataAccess;
using Mcs.DataAccess.Interfaces;
using Mcs.Entities;
using Mcs.DataAccess.Repositories;
using Mcs.Core.Configuration;
using  Mcs.Core.Exceptions;
using Mcs.DataAccess.Enumerators;

namespace Mcs.BussinessLogic.Catalogos
{
    /// <summary>
    /// Clase de Negocio para el catálogo de Asentamientos.
    /// </summary>
    public class Asentamientos
    {
        //private IConnectionFactory connectionFactory;


        /// <summary>
        /// Obtiene el/los Asentamientos dependiendo de los filtros enviados.
        /// Estos pueden ser por Id de la Entidad Federativa, Id del Municipio o por Código Postal.
        /// </summary>
        /// <param name="request">Parametro de entrada que contiene los filtros de busqueda.</param>
        /// <returns>Clase respuesta que contiene la información soliciatada, en este caso el/los asentamientos.</returns>
        public ResponseDto<List<BeAsentamiento>> GetAsentamientos(BeAsentamiento request)
        {
            ResponseDto<List<BeAsentamiento>> result;
            try
            {
                if (request == null) throw new Exception(Messages.NullParameterException);
                if (request.IdEstado <= 0 && request.IdMunicipio <= 0 && string.IsNullOrEmpty(request.CodigoPostal)) throw new Exception("Los parámetros de estado y municipio y/o el Código postal no pueden ser vacios o nulos.");
                if (request.IdEstado > 0 && request.IdMunicipio <= 0 ) throw new Exception("El parámetro de municipio no pueden ser vacio o nulo.");
                if (request.IdEstado <= 0 && string.IsNullOrEmpty(request.CodigoPostal)) throw new Exception("El estado o el Código postal no pueden ser vacios o nulos.");

                var listDataResult = new AsentamientoRepository(null).GetAsentamientos(request);

                result = new ResponseDto<List<BeAsentamiento>>
                {
                    Success = true,
                    TotalRows = listDataResult?.Count ?? 0,
                    Value = (List<BeAsentamiento>) listDataResult
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("User01", "MSC", "Asentamientos", ex);
                result = new ResponseDto<List<BeAsentamiento>>
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
