namespace GOB.SPF.ConecII.AccessData
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Schemas;

    public class RepositoryCuotas : IRepository<Cuota>
    {
        private int pages { get; set; }
        public int Pages { get { return pages; } }

        private UnitOfWorkCatalog _unitOfWork;

        public RepositoryCuotas(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;

            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public IEnumerable<Cuota> Obtener(Paging paging)
        {
            var result = new List<Cuota>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = paging.CurrentPage, ParameterName = "@pagina" });
                cmd.Parameters.Add(new SqlParameter() { Value = paging.Rows, ParameterName = "@filas" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cuota cuotas = new Cuota();

                        cuotas.Identificador = Convert.ToInt32(reader["IdCuota"]);

                        cuotas.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        cuotas.TipoServicio = Convert.ToString(reader["TipoServicio"]);

                        cuotas.IdReferencia = Convert.ToInt32(reader["IdReferencia"]);
                        cuotas.Referencia = Convert.ToString(reader["Referencia"]);

                        cuotas.IdDependencia = Convert.ToInt32(reader["IdDependencia"]);
                        cuotas.Dependencia = Convert.ToString(reader["Dependencia"]);
                        cuotas.DescripcionDependencia = Convert.ToString(reader["DescripcionDependencia"]);

                        cuotas.Concepto = Convert.ToString(reader["Concepto"]);

                        cuotas.IdJerarquia = Convert.ToInt32(reader["IdJerarquia"]);
                        cuotas.Jerarquia = Convert.ToString(reader["Jerarquia"]);

                        cuotas.IdGrupoTarifario = Convert.ToInt32(reader["IdGrupoTarifario"]);
                        cuotas.GrupoTarifario = Convert.ToString(reader["GrupoTarifario"]);

                        cuotas.CuotaBase = Convert.ToDecimal(reader["CuotaBase"]);
                        cuotas.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        cuotas.MedidaCobro = Convert.ToString(reader["MedidaCobro"]);

                        cuotas.Iva = Convert.ToDecimal(reader["Iva"]);
                        cuotas.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        cuotas.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        cuotas.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        cuotas.FechaPublicaDof = Convert.ToDateTime(reader["FechaPublicaDof"]);
                        cuotas.Activo = Convert.ToBoolean(reader["Activo"]);

                        result.Add(cuotas);
                    }
                }
                return result;  // yield?
            }

        }

        public Cuota ObtenerPorId(long id)
        {
            int result = 0;
            Cuota cuotas = null;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasObtenerId;
                cmd.CommandType = CommandType.StoredProcedure;

                // Adiciona um parâmetetro via IDbCommand, para evitar SqlInjection
                SqlParameter parameter = new SqlParameter()
                {
                    Value = id,
                    ParameterName = "@Identificador"
                };

                cmd.Parameters.Add(parameter);

                cuotas = new Cuota();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cuotas.Identificador = Convert.ToInt32(reader["IdCuota"]);

                        cuotas.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        cuotas.TipoServicio = Convert.ToString(reader["TipoServicio"]);

                        cuotas.IdReferencia = Convert.ToInt32(reader["IdReferencia"]);
                        cuotas.Referencia = Convert.ToString(reader["Referencia"]);

                        cuotas.IdDependencia = Convert.ToInt32(reader["IdDependencia"]);
                        cuotas.Dependencia = Convert.ToString(reader["Dependencia"]);
                        cuotas.DescripcionDependencia = Convert.ToString(reader["DescripcionDependencia"]);

                        cuotas.Concepto = Convert.ToString(reader["Concepto"]);

                        cuotas.IdJerarquia = Convert.ToInt32(reader["IdJerarquia"]);
                        cuotas.Jerarquia = Convert.ToString(reader["Jerarquia"]);

                        cuotas.IdGrupoTarifario = Convert.ToInt32(reader["IdGrupoTarifario"]);
                        cuotas.GrupoTarifario = Convert.ToString(reader["GrupoTarifario"]);

                        cuotas.CuotaBase = Convert.ToDecimal(reader["CuotaBase"]);
                        cuotas.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        cuotas.MedidaCobro = Convert.ToString(reader["MedidaCobro"]);

                        cuotas.Iva = Convert.ToDecimal(reader["Iva"]);
                        cuotas.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        cuotas.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        cuotas.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        cuotas.FechaPublicaDof = Convert.ToDateTime(reader["FechaPublicaDof"]);
                        cuotas.Activo = Convert.ToBoolean(reader["Activo"]);

                    }
                }
            }

            return cuotas;
        }

        public int CambiarEstatus(Cuota entity)
        {
            int result = 0;
            // Inicia nosso objeto de comando. 
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = Catalogos.PeriodosCambiarEstatus;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                result = cmd.ExecuteNonQuery();

                //Comita nossa transação.
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Insertar(Cuota entity)
        {
            int result = 0;
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasInsertar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdReferencia, ParameterName = "@IdReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdDependencia, ParameterName = "@IdDependencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Concepto, ParameterName = "@Concepto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdJerarquia, ParameterName = "@IdJerarquia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdGrupoTarifario, ParameterName = "@IdGrupoTarifario" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.CuotaBase, ParameterName = "@CuotaBase" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdMedidaCobro, ParameterName = "@IdMedidaCobro" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Iva, ParameterName = "@Iva" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaAutorizacion, ParameterName = "@FechaAutorizacion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaEntradaVigor, ParameterName = "@FechaEntradaVigor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaTermino, ParameterName = "@FechaTermino" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaPublicaDof, ParameterName = "@FechaPublicaDof" });

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public int Actualizar(Cuota entity)
        {
            int result = 0;

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasActualizar;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdReferencia, ParameterName = "@IdReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdDependencia, ParameterName = "@IdDependencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Concepto, ParameterName = "@Concepto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdJerarquia, ParameterName = "@IdJerarquia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdGrupoTarifario, ParameterName = "@IdGrupoTarifario" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.CuotaBase, ParameterName = "@CuotaBase" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdMedidaCobro, ParameterName = "@IdMedidaCobro" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Iva, ParameterName = "@Iva" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaAutorizacion, ParameterName = "@FechaAutorizacion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaEntradaVigor, ParameterName = "@FechaEntradaVigor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaTermino, ParameterName = "@FechaTermino" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaPublicaDof, ParameterName = "@FechaPublicaDof" });

                result = cmd.ExecuteNonQuery();

                //_unitOfWork.SaveChanges();

                return result;
            }
        }

        public IEnumerable<Cuota> ObtenerPorCriterio(Paging paging, Cuota entity)
        {
            var result = new List<Cuota>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Activo, ParameterName = "@Activo" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cuota cuotas = new Cuota();

                        cuotas.Identificador = Convert.ToInt32(reader["IdCuota"]);

                        cuotas.IdTipoServicio = Convert.ToInt32(reader["IdTipoServicio"]);
                        cuotas.TipoServicio = Convert.ToString(reader["TipoServicio"]);

                        cuotas.IdReferencia = Convert.ToInt32(reader["IdReferencia"]);
                        cuotas.Referencia = Convert.ToString(reader["Referencia"]);

                        cuotas.IdDependencia = Convert.ToInt32(reader["IdDependencia"]);
                        cuotas.Dependencia = Convert.ToString(reader["Dependencia"]);
                        cuotas.DescripcionDependencia = Convert.ToString(reader["DescripcionDependencia"]);

                        cuotas.Concepto = Convert.ToString(reader["Concepto"]);

                        cuotas.IdJerarquia = Convert.ToInt32(reader["IdJerarquia"]);
                        cuotas.Jerarquia = Convert.ToString(reader["Jerarquia"]);

                        cuotas.IdGrupoTarifario = Convert.ToInt32(reader["IdGrupoTarifario"]);
                        cuotas.GrupoTarifario = Convert.ToString(reader["GrupoTarifario"]);

                        cuotas.CuotaBase = Convert.ToDecimal(reader["CuotaBase"]);
                        cuotas.IdMedidaCobro = Convert.ToInt32(reader["IdMedidaCobro"]);
                        cuotas.MedidaCobro = Convert.ToString(reader["MedidaCobro"]);

                        cuotas.Iva = Convert.ToDecimal(reader["Iva"]);
                        cuotas.FechaAutorizacion = Convert.ToDateTime(reader["FechaAutorizacion"]);
                        cuotas.FechaEntradaVigor = Convert.ToDateTime(reader["FechaEntradaVigor"]);
                        cuotas.FechaTermino = Convert.ToDateTime(reader["FechaTermino"]);
                        cuotas.FechaPublicaDof = Convert.ToDateTime(reader["FechaPublicaDof"]);
                        cuotas.Activo = Convert.ToBoolean(reader["Activo"]);


                        result.Add(cuotas);
                    }
                }
                return result;  // yield?
            }
        }

        public string ValidarRegistro( Cuota entity)
        {
            string result = "";

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Catalogos.CuotasValidarRegistro;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter() { Value = entity.Identificador, ParameterName = "@Identificador" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoServicio, ParameterName = "@IdTipoServicio" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdReferencia, ParameterName = "@IdReferencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdDependencia, ParameterName = "@IdDependencia" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Concepto, ParameterName = "@Concepto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaAutorizacion, ParameterName = "@FechaAutorizacion" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaEntradaVigor, ParameterName = "@FechaEntradaVigor" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaTermino, ParameterName = "@FechaTermino" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.FechaPublicaDof, ParameterName = "@FechaPublicaDof" });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["Resultado"].ToString();
                    }
                }
                //_unitOfWork.SaveChanges();

                return result;
            }
        }

    }
}
