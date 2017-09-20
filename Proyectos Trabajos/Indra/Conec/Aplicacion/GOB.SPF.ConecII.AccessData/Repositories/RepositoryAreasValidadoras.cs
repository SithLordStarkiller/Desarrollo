namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using System.Collections.Generic;
    using System;

    using System.Data;
    using System.Linq;
    using System.Data.SqlClient;

    using Schemas;
    using Entities;

    public class RepositoryAreasValidadoras : IRepository<AreasValidadoras>
    {
        public int Pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryAreasValidadoras(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Insertar(AreasValidadoras entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasInsertarSingle;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdActividad, ParameterName = "@IdActividad" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.IdCentroCosto, ParameterName = "@IdCentroCosto" });
                cmd.Parameters.Add(new SqlParameter { Value = entity.Obligatorio, ParameterName = "@Obligatorio" });

                var dataReader = cmd.ExecuteNonQuery();
                return dataReader;
            }
        }

        public AreasValidadoras ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(AreasValidadoras entity)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasModificarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio", SqlDbType = SqlDbType.Structured });
                cmd.Parameters.Add(new SqlParameter { Value = entity.EsActivo, ParameterName = "@Activo", SqlDbType = SqlDbType.Structured });

                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                var lista = dataTable.AsEnumerable().Select(row =>
                    new AreasValidadoras
                    {
                        IdTipoServicio = row.Field<int>("IdTipoServicio"),
                        IdActividad = row.Field<int>("IdActividad"),
                        IdCentroCosto = row.Field<string>("IdCentroCosto"),
                        Obligatorio = row.Field<bool>("Obligatorio"),
                        EsActivo = row.Field<bool>("EsActivo"),

                    }).ToList();

                return lista.Count;
            }
        }

        public int Actualizar(AreasValidadoras entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AreasValidadoras> Obtener(Paging paging)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                var lista = dataTable.AsEnumerable().Select(row =>
                    new AreasValidadoras
                    {
                        IdAreasValidadoras = row.Field<int>("IdAreaValidadora"),
                        IdTipoServicio = row.Field<int>("IdTipoServicio"),
                        TipoServicio = row.Field<string>("TipoServicio"),
                        IdActividad = row.Field<int>("IdActividad"),
                        IdCentroCosto = row.Field<string>("IdCentroCosto"),
                        Obligatorio = row.Field<bool>("Obligatorio"),
                        EsActivo = row.Field<bool>("Activo")

                    }).ToList();

                return lista;
            }
        }

        public IEnumerable<AreasValidadoras> ObtenerPorCriterio(Paging paging, AreasValidadoras entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertarTabla(DataTable table)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = table, ParameterName = "@TablaAreaValidadora", SqlDbType = SqlDbType.Structured, TypeName = "Configuracion.AreaValidadora" });

                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                return true;
            }
        }
        public bool ActualizarTabla(DataTable table)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = table, ParameterName = "@TablaAreaValidadora", SqlDbType = SqlDbType.Structured });

                var uspResult = cmd.ExecuteReader();


                return uspResult != null;
            }
        }

        public bool Eliminar(int idTipoServicio)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.AreasValidadorasEliminar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = idTipoServicio, ParameterName = "@IdTipoServicio" });

                var uspResult = cmd.ExecuteNonQuery();


                return uspResult > 0;
            }
        }
    }
}