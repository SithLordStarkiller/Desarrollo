using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryFracciones : IRepository<Fraccion>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryFracciones(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Fraccion> Obtener(IPaging paging)
        {
            var result = new List<Fraccion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.All, ParameterName = "@Todos" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Fraccion fracciones = new Fraccion();

                        fracciones.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        fracciones.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        fracciones.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        fracciones.Nombre = reader["Nombre"].ToString();
                        fracciones.Descripcion = reader["Descripcion"].ToString();
                        fracciones.FechaInicial = reader["FechaInicial"] != DBNull.Value ? Convert.ToDateTime(reader["FechaInicial"]) : DateTime.Today.Date;
                        fracciones.FechaFinal = reader["FechaFinal"] != DBNull.Value ? Convert.ToDateTime(reader["FechaFinal"]) : DateTime.Today.Date;
                        fracciones.Activo = Convert.ToBoolean(reader["Activo"]);
                        fracciones.Grupo = reader["Grupo"].ToString();
                        fracciones.Division = reader["Division"].ToString();

                        result.Add(fracciones);
                    }
                    reader.Close();
                }
                return result;  // yield?
            }

        }

        public Fraccion ObtenerPorId(long id)
        {
            int result = 0;
            Fraccion fracciones = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                fracciones = new Fraccion();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fracciones.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        fracciones.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        fracciones.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        fracciones.Nombre = reader["Nombre"].ToString();
                        fracciones.Descripcion = reader["Descripcion"].ToString();
                        fracciones.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        fracciones.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        fracciones.Activo = Convert.ToBoolean(reader["Activo"]);
                        fracciones.Grupo = reader["Grupo"].ToString();
                        fracciones.Division = reader["Division"].ToString();
                    }
                    reader.Close();
                }
            }

            return fracciones;
        }

        public int CambiarEstatus(Fraccion fracciones)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = fracciones.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = fracciones.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Fraccion fracciones)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[6];

                parameters[0] = new SqlParameter() { Value = fracciones.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = fracciones.Descripcion, ParameterName = "@Descripcion" };
                parameters[2] = new SqlParameter() { Value = fracciones.IdGrupo, ParameterName = "@IdGrupo" };
                parameters[3] = new SqlParameter() { Value = DateTime.Today, ParameterName = "@FechaInicial" };
                parameters[4] = new SqlParameter() { Value = DateTime.Today, ParameterName = "@FechaFinal" };
                parameters[5] = new SqlParameter() { Value = true, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);
                cmd.Parameters.Add(parameters[5]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Fraccion fracciones)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = fracciones.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = fracciones.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = fracciones.Descripcion, ParameterName = "@Descripcion" };
                parameters[3] = new SqlParameter() { Value = fracciones.IdGrupo, ParameterName = "@IdGrupo" };
                parameters[4] = new SqlParameter() { Value = DateTime.Today.Date, ParameterName = "@FechaFinal" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);
                cmd.Parameters.Add(parameters[3]);
                cmd.Parameters.Add(parameters[4]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Fraccion> ObtenerPorCriterio(IPaging paging, Fraccion entity)
        {
            var result = new List<Fraccion>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Fraccion fracciones = new Fraccion();

                        fracciones.Identificador = Convert.ToInt32(reader["IdFraccion"]);
                        fracciones.IdGrupo = Convert.ToInt32(reader["IdGrupo"]);
                        fracciones.IdDivision = Convert.ToInt32(reader["IdDivision"]);
                        fracciones.Nombre = reader["Nombre"].ToString();
                        fracciones.Descripcion = reader["Descripcion"].ToString();
                        fracciones.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            fracciones.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        fracciones.Activo = Convert.ToBoolean(reader["Activo"]);
                        fracciones.Grupo = reader["Grupo"].ToString();
                        fracciones.Division = reader["Division"].ToString();

                        result.Add(fracciones);
                    }
                    reader.Close();
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro(Fraccion fracciones)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.FraccionesValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[3];

                parameters[0] = new SqlParameter() { Value = fracciones.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = fracciones.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = fracciones.IdGrupo, ParameterName = "@IdGrupo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                    reader.Close();
                }
                //_unitOfWork.SaveChanges();

            }

            return result;
        }

    }
}
