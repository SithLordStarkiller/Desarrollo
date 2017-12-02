using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryTiposControl : IRepository<TipoControl>
    {
        #region variables privadas
        private int pages { get; set; }
        private UnitOfWorkCatalog _unitOfWork;
        #endregion

        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
        public RepositoryTiposControl(IUnitOfWork uow)
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

        #region metodos publicos
        public IEnumerable<TipoControl> Obtener(IPaging paging)
        {
            var result = new List<TipoControl>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposControlObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoControl TipoControl = new TipoControl();

                        TipoControl.Identificador = Convert.ToInt32(reader["IdTipoControl"]);
                        TipoControl.Nombre = reader["Nombre"].ToString();
                        TipoControl.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (reader["FechaFinal"] != DBNull.Value)
                            TipoControl.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        TipoControl.Activo = Convert.ToBoolean(reader["Activo"]);
                        
                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(TipoControl);
                    }
                }
                return result;
            }
        }

        public TipoControl ObtenerPorId(long id)
        {
            int result = 0;
            TipoControl TipoControl = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposControlObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                TipoControl = new TipoControl();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoControl.Identificador = Convert.ToInt32(reader["IdTipoControl"]);
                        TipoControl.Nombre = reader["Nombre"].ToString();
                        TipoControl.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return TipoControl;
        }

        public int CambiarEstatus(TipoControl objeto)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposControlCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = objeto.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = objeto.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(TipoControl objeto)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = objeto.Nombre, ParameterName = "@Nombre" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(TipoControl objeto)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposControlActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = objeto.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = objeto.Nombre, ParameterName = "@Nombre" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<TipoControl> ObtenerPorCriterio(IPaging paging, TipoControl entity)
        {
            var result = new List<TipoControl>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.TiposControlObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoControl prop = new TipoControl();

                        prop.Identificador = Convert.ToInt32(reader["IdTipoControl"]);
                        prop.Nombre = reader["Nombre"].ToString();
                        prop.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            prop.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        prop.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(prop);
                    }
                }
                return result;  // yield?
            }
        }
        #endregion
    }
}
