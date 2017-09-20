namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;

    using Schemas;
    using Entities;

    public class RepositoryReceptoresAlertas : IRepository<ReceptorAlerta>
    {
        #region propiedades

        public int Pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryReceptoresAlertas(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        #endregion

        #region Metodos del CRUD

        public int Insertar(ReceptorAlerta entity)
        {
            throw new NotImplementedException();
        }

        public ReceptorAlerta ObtenerPorId(long identificador)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(ReceptorAlerta entity)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(ReceptorAlerta entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReceptorAlerta> Obtener(Paging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReceptorAlerta> ObtenerPorCriterio(Paging paging, ReceptorAlerta entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Metodos Especiales

        public bool InsertarListaReceptorAlerta(DataTable tablaReceptorAlerta, Notificaciones notificacion)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.ReceptorAlertaInsertarTabla;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = notificacion.IdTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.IdActividad, ParameterName = "@IdActividad" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.IdFase, ParameterName = "@IdFase" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.CuerpoCorreo, ParameterName = "@CuerpoCorreo" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.EsCorreo, ParameterName = "@EsCorreo" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.EsSistema, ParameterName = "@EsSistema" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.EmitirAlerta, ParameterName = "@EmitirAlerta" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.TiempoAlerta, ParameterName = "@TiempoAlerta" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.Frecuencia, ParameterName = "@Frecuencia" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.AlertaEsCorreo, ParameterName = "@AlertaEsCorreo" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.AlertaEsSistema, ParameterName = "@AlertaEsSistema" });
                cmd.Parameters.Add(new SqlParameter { Value = notificacion.CuerpoAlerta, ParameterName = "@CuerpoAlerta" });

                cmd.Parameters.Add(new SqlParameter { Value = tablaReceptorAlerta, ParameterName = "@ReceptorAlerta", SqlDbType = SqlDbType.Structured });

                var uspResult = cmd.ExecuteReader();
                var lista = new List<ReceptorAlerta>();

                using (uspResult)
                {
                    while (uspResult.Read())
                    {
                        var receptor = new ReceptorAlerta
                        {
                            IdNotificacion = Convert.ToInt32(uspResult["IdNotificacion"]),
                            IdPersona = new Guid(uspResult["IdPersona"].ToString()),
                            IdRol = Convert.ToInt32(uspResult["IdRol"]),
                            IdUsuario = Convert.ToInt32(uspResult["IdUsuario"]),
                            IdTipoReceptor = Convert.ToInt32(uspResult["IdTipoReceptor"]),
                            Correo = Convert.ToString(uspResult["Correo"])
                        };
                        lista.Add(receptor);
                    }
                }

                return lista.Count > 0;
            }
        }

        public List<ReceptorAlerta> ListaReceptorAlertaObtenerTodos(Notificaciones notificaciones)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.ReceptorAlertasNotificacionesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = notificaciones.IdNotificacion, ParameterName = "@IdNotificacion" });

                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                var lista = dataTable.AsEnumerable().Select(row =>
                    new ReceptorAlerta
                    {
                        IdNotificacion = row.Field<int>("IdNotificacion"),
                        IdPersona = string.IsNullOrEmpty(row["IdPersona"].ToString())? new Guid() : row.Field<Guid>("IdPersona"),
                        IdRol = row.Field<int?>("IdRol"),
                        IdTipoReceptor = row.Field<int?>("IdTipoReceptor"),
                        IdUsuario = row.Field<int?>("IdUsuario")

                    }).ToList();

                return lista;
            }
        }

        #endregion
    }
}
