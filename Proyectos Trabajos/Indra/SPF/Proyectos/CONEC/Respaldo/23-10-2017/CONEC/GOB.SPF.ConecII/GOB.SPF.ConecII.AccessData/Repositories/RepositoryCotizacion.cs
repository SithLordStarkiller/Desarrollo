using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Library;


namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryCotizacion : IRepository<Cotizacion>
    {
        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Constructor

        public RepositoryCotizacion(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }


        #endregion

        #region Interface IRepository

        public int Actualizar(Cotizacion entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Cotizacion entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Cotizacion entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> ObtenerPorCriterio(IPaging paging, Cotizacion entity)
        {
            throw new NotImplementedException();
        }

        public Cotizacion ObtenerPorId(long Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.Solicitud_CotizacionObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdCotizacion" });
                var cotizacion = new Cotizacion();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cotizacion = LeerCotizacion(reader);
                    }

                    return cotizacion;
                }
            }
        }

        private static Cotizacion LeerCotizacion(IDataRecord reader)
        {
            var cotizacion = new Cotizacion
            {
                Identificador = reader["IdCotizacion"].To<int>(),
                IdCotizacionAnterior = reader["IdCotizacionAnterior"].To<int>(),
                Folio = reader["Folio"].To<string>(),
                EsValida = reader["EsValida"].To<bool>(),
                Documento = reader["Documento"].To<int>(),
                Firma = reader["Firma"].To<byte[]>(),
                FechaInicial = reader["FechaInicial"].To<DateTime>(),
                FechaFinal = reader["FechaFinal"].To<DateTime>()
            };

            return cotizacion;
        }

        #endregion

        /// <summary>
        /// Guarda la fima digital en la cotizacion
        /// </summary>
        /// <param name="identificador">IdCotizacion</param>
        /// <param name="firma"></param>
        /// <returns></returns>
        public bool GuardarFirma(int identificador, byte[] firma)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.Solicitud_CotizacionesFirmaActualizar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = identificador, ParameterName = "@IdCotizacion" });
                cmd.Parameters.Add(new SqlParameter() { Value = firma, ParameterName = "@Firma" });
                var result =   cmd.ExecuteNonQuery();
            }

            return true;
          
        }
    }
}
