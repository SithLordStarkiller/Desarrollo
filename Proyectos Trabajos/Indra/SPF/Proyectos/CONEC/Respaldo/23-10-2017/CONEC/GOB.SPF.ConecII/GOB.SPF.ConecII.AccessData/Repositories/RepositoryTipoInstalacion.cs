using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryTipoInstalacion : IRepository<TipoInstalacion>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryTipoInstalacion(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<TipoInstalacion> Obtener(IPaging paging)
        {
            var result = new List<TipoInstalacion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Solicitud.TipoInstalacionObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" };
                parameters[1] = new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoInstalacion tipoInstalacion = new TipoInstalacion();

                        tipoInstalacion.Identificador = Convert.ToInt32(reader["IdTipoInstalacion"]);
                        tipoInstalacion.Nombre = reader["Nombre"].ToString();

                        result.Add(tipoInstalacion);
                    }
                }
                return result;  // yield?
            }

        }

        public IEnumerable<TipoInstalacion> ObtenerPorCriterio(IPaging paging, TipoInstalacion entity)
        {
            var result = new List<TipoInstalacion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Solicitud.TipoInstalacionObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@IdTipoInstalacion" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoInstalacion tipoInstalacion = new TipoInstalacion();

                        tipoInstalacion.Identificador = Convert.ToInt32(reader["IdTipoInstalacion"]);
                        tipoInstalacion.Nombre = reader["Nombre"].ToString();

                        result.Add(tipoInstalacion);
                    }
                }
                return result;  // yield?
            }
        }
        public TipoInstalacion ObtenerPorId(long id)
        {
            int result = 0;
            TipoInstalacion tipoInstalacion = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                //cmd.CommandText = Solicitud.TipoInstalacionObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                tipoInstalacion = new TipoInstalacion();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoInstalacion.Identificador = Convert.ToInt32(reader["IdTipoInstalacion"]);
                        tipoInstalacion.Nombre = reader["Nombre"].ToString();
                    }
                }
            }

            return tipoInstalacion;
        }

        public int CambiarEstatus(TipoInstalacion tipoInstalacion)
        {
            int result = 0;

            return result;
        }    

        public int Insertar(TipoInstalacion entity)
        {
            throw new NotImplementedException();
        }

        TipoInstalacion IRepository<TipoInstalacion>.ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(TipoInstalacion entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<TipoInstalacion> IRepository<TipoInstalacion>.Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }
    }
}