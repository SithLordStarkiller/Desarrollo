namespace GOB.SPF.ConecII.AccessData.Repositories
{
    using System.Collections.Generic;
    using System;

    using System.Data;
    using System.Data.SqlClient;

    using Schemas;
    using Entities;

    public class RepositoryNotificacionesAlertas : IRepository<Notificaciones>
    {
        public int Pages { get; set; }

        private readonly UnitOfWorkCatalog _unitOfWork;

        public RepositoryNotificacionesAlertas(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int Insertar(Notificaciones entity)
        {
            return 1;
            //using (var cmd = _unitOfWork.CreateCommand())
            //{
            //    cmd.CommandText = Configuracion.NotificacionesInsertar;
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.Add(new SqlParameter { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.IdActividad, ParameterName = "@IdActividad" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.IdFase, ParameterName = "@IdFase" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.CuerpoCorreo, ParameterName = "@CuerpoCorreo" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.EsCorreo, ParameterName = "@EsCorreo" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.EsSistema, ParameterName = "@EsSistema" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.EmitirAlerta, ParameterName = "@EmitirAlerta" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.TiempoAlerta, ParameterName = "@TiempoAlerta" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.Frecuencia, ParameterName = "@Frecuencia" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.AlertaEsCorreo, ParameterName = "@AlertaEsCorreo" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.AlertaEsSistema, ParameterName = "@AlertaEsSistema" });
            //    cmd.Parameters.Add(new SqlParameter { Value = entity.CuerpoAlerta, ParameterName = "@CuerpoAlerta" });


            //    var result = cmd.ExecuteNonQuery();

            //    return result;
            //}
        }

        public Notificaciones ObtenerPorId(long Identificador)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.NotificacionesObtenerPorId;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { Value = Identificador, ParameterName = "@IdNotificacion" });

                var uspResult = cmd.ExecuteReader();

                using (uspResult)
                {
                    while (uspResult.Read())
                    {
                        var notificacion = new Notificaciones
                        {
                            IdNotificacion = Convert.ToInt32(uspResult["IdNotificacion"]),
                            IdTipoServicio = Convert.ToInt32(uspResult["IdTipoServicio"]),
                            TipoServicio = Convert.ToString(uspResult["TipoServicio"]),
                            IdActividad = Convert.ToInt32(uspResult["IdActividad"]),
                            Actividad = Convert.ToString(uspResult["Actividad"]),
                            IdFase = Convert.ToInt32(uspResult["IdFase"]),
                            Fase = Convert.ToString(uspResult["Fase"]),
                            CuerpoCorreo = Convert.ToString(uspResult["CuerpoCorreo"]),
                            EsCorreo = Convert.ToBoolean(uspResult["EsCorreo"]),
                            EsSistema = Convert.ToBoolean(uspResult["EsSistema"]),
                            EmitirAlerta = Convert.ToBoolean(uspResult["EmitirAlerta"]),
                            TiempoAlerta = Convert.ToInt32(uspResult["TiempoAlerta"]),
                            Frecuencia = Convert.ToInt32(uspResult["Frecuencia"]),
                            AlertaEsCorreo = Convert.ToBoolean(uspResult["AlertaEsCorreo"]),
                            AlertaEsSistema = Convert.ToBoolean(uspResult["AlertaEsSistema"]),
                            CuerpoAlerta = Convert.ToString(uspResult["CuerpoAlerta"]),
                            Activo = Convert.ToBoolean(uspResult["Activo"])
                        };
                        return notificacion;
                    }
                }

                return null;
            }
        }

        public int CambiarEstatus(Notificaciones entity)
        {
            throw new System.NotImplementedException();
        }

        public int Actualizar(Notificaciones entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Notificaciones> Obtener(Paging paging)
        {
            var lista = new List<Notificaciones>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Configuracion.NotificacionesObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add(new SqlParameter { Value = paging.CurrentPage, ParameterName = "@pagina" });
                //cmd.Parameters.Add(new SqlParameter { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var notificacion = new Notificaciones
                        {
                            IdNotificacion = Convert.ToInt32(reader["IdNotificacion"]),
                            IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]),
                            TipoServicio = Convert.ToString(reader["TipoServicio"]),
                            IdActividad = Convert.ToInt32(reader["IdActividad"]),
                            Actividad = Convert.ToString(reader["Actividad"]),
                            IdFase = Convert.ToInt32(reader["IdFase"]),
                            Fase = Convert.ToString(reader["Fase"]),
                            CuerpoCorreo = Convert.ToString(reader["CuerpoCorreo"]),
                            EsCorreo = Convert.ToBoolean(reader["EsCorreo"]),
                            EsSistema = Convert.ToBoolean(reader["EsSistema"]),
                            EmitirAlerta = Convert.ToBoolean(reader["EmitirAlerta"]),
                            TiempoAlerta = Convert.ToInt32(reader["TiempoAlerta"]),
                            Frecuencia = Convert.ToInt32(reader["Frecuencia"]),
                            AlertaEsCorreo = Convert.ToBoolean(reader["AlertaEsCorreo"]),
                            AlertaEsSistema = Convert.ToBoolean(reader["AlertaEsSistema"]),
                            CuerpoAlerta = Convert.ToString(reader["CuerpoAlerta"]),
                            Activo = Convert.ToBoolean(reader["Activo"])
                        };
                        lista.Add(notificacion);
                    }
                }
            }

            return lista;
        }

        public IEnumerable<Notificaciones> ObtenerPorCriterio(Paging paging, Notificaciones entity)
        {
            return null;
        }
    }
}
