using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;
    public class RepositoryControles : IRepository<Control>
    {
        #region variables privadas
        private int pages { get; set; }
        private UnitOfWorkCatalog _unitOfWork;
        #endregion

        #region variables publicas
        public int Pages { get { return pages; } }
        #endregion

        #region constructor
        public RepositoryControles(IUnitOfWork uow)
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
        public IEnumerable<Control> Obtener(IPaging paging)
        {
            var result = new List<Control>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Control Control = new Control();

                        Control.Identificador = Convert.ToInt32(reader["IdControl"]);
                        Control.IdTipoControl = Convert.ToInt32(reader["IdTipoControl"]);
                        Control.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        Control.Nombre = reader["Nombre"].ToString();
                        Control.Descripcion = reader["Descripcion"].ToString();
                        Control.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (reader["FechaFinal"] != DBNull.Value)
                            Control.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Control.Activo = Convert.ToBoolean(reader["Activo"]);

                        if (reader["Paginas"] != null)
                            pages = Convert.ToInt32(reader["Paginas"]);

                        result.Add(Control);
                    }
                }
                return result;
            }
        }

        public Control ObtenerPorId(long id)
        {
            int result = 0;
            Control Control = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                Control = new Control();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Control.Identificador = Convert.ToInt32(reader["IdControl"]);
                        Control.IdTipoControl = Convert.ToInt32(reader["IdTipoControl"]);
                        Control.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        Control.Nombre = reader["Nombre"].ToString();
                        Control.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            Control.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Control.Activo = Convert.ToBoolean(reader["Activo"]);
                    }
                }
            }

            return Control;
        }

        public int CambiarEstatus(Control Control)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = Control.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public int Insertar(Control Control)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = Control.IdTipoControl, ParameterName = "@IdTipoControl" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.IdModulo, ParameterName = "@IdModulo" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public int Actualizar(Control Control)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = Control.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.IdModulo, ParameterName = "@IdModulo" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.IdTipoControl, ParameterName = "@IdTipoControl" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = Control.Descripcion, ParameterName = "@Descripcion" });

                result = cmd.ExecuteNonQuery();

                return result;
            }
        }

        public IEnumerable<Control> ObtenerPorCriterio(IPaging paging, Control entity)
        {
            var result = new List<Control>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Seguridad.ControlesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Control Control = new Control();

                        Control.Identificador = Convert.ToInt32(reader["IdControl"]);
                        Control.IdTipoControl = Convert.ToInt32(reader["IdTipoControl"]);
                        Control.IdModulo = Convert.ToInt32(reader["IdModulo"]);
                        Control.Nombre = reader["Nombre"].ToString();
                        Control.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            Control.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        Control.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(Control);
                    }
                }
                return result;  // yield?
            }
        }
        #endregion
    }
}
