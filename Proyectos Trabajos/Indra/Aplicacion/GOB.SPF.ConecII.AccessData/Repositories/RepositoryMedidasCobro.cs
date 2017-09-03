namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryMedidasCobro : IRepository<MedidaCobro>
    {
        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryMedidasCobro(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<MedidaCobro> Obtener(Paging paging)
        {
            var result = new List<MedidaCobro>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MedidaCobro medidasCobro = new MedidaCobro();

                        medidasCobro.Identificador = Convert.ToInt32(reader["IdMedidaCobro"]);
                        medidasCobro.Nombre = Convert.ToString(reader["Nombre"]);
                        medidasCobro.Descripcion = Convert.ToString(reader["Descripcion"]);
                        medidasCobro.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            medidasCobro.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        medidasCobro.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(medidasCobro);
                    }
                }
                return result;  // yield?
            }

        }

        public MedidaCobro ObtenerPorId(long id)
        {
            int result = 0;
            MedidaCobro medidasCobro = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                medidasCobro = new MedidaCobro();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medidasCobro.Identificador = Convert.ToInt32(reader["IdMedidaCobro"]);
                        medidasCobro.Nombre = Convert.ToString(reader["Nombre"]);
                        medidasCobro.Descripcion = Convert.ToString(reader["Descripcion"]);
                        medidasCobro.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            medidasCobro.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        medidasCobro.Activo = Convert.ToBoolean(reader["Activo"]);


                    }
                }
            }

            return medidasCobro;
        }

        public int CambiarEstatus(MedidaCobro medidasCobro)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[2];

                parameters[0] = new SqlParameter() { Value = medidasCobro.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = medidasCobro.Activo, ParameterName = "@Activo" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(MedidaCobro medidasCobro)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter() { Value = medidasCobro.Nombre, ParameterName = "@Nombre" };
                parameters[1] = new SqlParameter() { Value = medidasCobro.Descripcion, ParameterName = "@Descripcion" };
                parameters[2] = new SqlParameter() { Value = medidasCobro.FechaInicial, ParameterName = "@FechaInicial" };
                parameters[3] = new SqlParameter() { Value = medidasCobro.FechaFinal, ParameterName = "@FechaFinal" };
                parameters[4] = new SqlParameter() { Value = medidasCobro.Activo, ParameterName = "@Activo" };

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

        public int Actualizar(MedidaCobro medidasCobro)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter() { Value = medidasCobro.Identificador, ParameterName = "@Identificador" };
                parameters[1] = new SqlParameter() { Value = medidasCobro.Nombre, ParameterName = "@Nombre" };
                parameters[2] = new SqlParameter() { Value = medidasCobro.Descripcion, ParameterName = "@Descripcion" };

                cmd.Parameters.Add(parameters[0]);
                cmd.Parameters.Add(parameters[1]);
                cmd.Parameters.Add(parameters[2]);

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<MedidaCobro> ObtenerPorCriterio(Paging paging, MedidaCobro entity)
        {
            var result = new List<MedidaCobro>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.MedidasCobroObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MedidaCobro medidasCobro = new MedidaCobro();

                        medidasCobro.Identificador = Convert.ToInt32(reader["IdMedidaCobro"]);
                        medidasCobro.Nombre = Convert.ToString(reader["Nombre"]);
                        medidasCobro.Descripcion = Convert.ToString(reader["Descripcion"]);
                        medidasCobro.FechaInicial = Convert.ToDateTime(reader["FechaInicial"]);
                        if (!(reader["FechaFinal"] == DBNull.Value))
                            medidasCobro.FechaFinal = Convert.ToDateTime(reader["FechaFinal"]);
                        medidasCobro.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(medidasCobro);
                    }
                }
                return result;  // yield?
            }
        }
    }
}
