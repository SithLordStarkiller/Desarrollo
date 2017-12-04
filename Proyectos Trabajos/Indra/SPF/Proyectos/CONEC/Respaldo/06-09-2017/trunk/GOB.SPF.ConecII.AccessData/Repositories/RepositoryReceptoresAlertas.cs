namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using System.Collections.Generic;
    using System;

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

        public ReceptorAlerta ObtenerPorId(long Identificador)
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

        public bool InsertarListaReceptorAlerta(DataTable tablaReceptorAlerta, int idNotificaciones)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.ReceptorAlertaInsertarTabla;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = tablaReceptorAlerta, ParameterName = "@ReceptorAlerta", SqlDbType = SqlDbType.Structured });

                //var result = cmd.ExecuteReader();

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

                return true;
            }
        }

        #endregion
    }
}
