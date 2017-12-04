using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Interfaces.Genericos;


namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryEnteroTesofe : IRepository<EnteroTesofe>
    {
        private int _pages { get; set; }
        public int Pages => _pages;

        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Constructor

        public RepositoryEnteroTesofe(IUnitOfWork uow)
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

        #region Metodos Publicos
        public bool PersistExcelEnterostesofe(List<EnteroTesofe> lista)
        {

            foreach (EnteroTesofe entity in lista)
            {
                EnteroTesofe enteroTesofe = null;

                //buscar el registro actual
                //llave NumeroOperacion y LlavePago
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_TesofeObtenerPorId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { Value = entity.NumeroOperacion, ParameterName = "@NumeroOperacion" });
                    cmd.Parameters.Add(new SqlParameter { Value = entity.LlavePago, ParameterName = "@LlavePago" });

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read()) //solo regresa 1 registro si ya existe
                        {
                            enteroTesofe = LeerEnteroTesofe(reader);
                        }
                    }
                }

                if (enteroTesofe == null)
                {
                    //insertar

                    using (var cmd = _unitOfWork.CreateCommand())
                    {
                        cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_TesofeInsertar;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter { Value = entity.NumeroOperacion, ParameterName = "@NumeroOperacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RFC, ParameterName = "@RFC" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CURP, ParameterName = "@CURP" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.FechaPresentacion, ParameterName = "@FechaPresentacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Sucursal, ParameterName = "@Sucursal" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.LlavePago, ParameterName = "@LlavePago" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Banco, ParameterName = "@Banco" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MedioRecepcion, ParameterName = "@MedioRecepcion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Dependencia, ParameterName = "@Dependencia" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Periodo, ParameterName = "@Periodo" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.SaldoFavor, ParameterName = "@SaldoFavor" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Importe, ParameterName = "@Importe" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ParteActualizada, ParameterName = "@ParteActualizada" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Recargos, ParameterName = "@Recargos" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MultaCorreccion, ParameterName = "@MultaCorreccion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Compensacion, ParameterName = "@Compensacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CantidadPagada, ParameterName = "@CantidadPagada" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ClaveReferenciaDPA, ParameterName = "@ClaveReferenciaDPA" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CadenaDependencia, ParameterName = "@CadenaDependencia" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ImporteIVA, ParameterName = "@ImporteIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ParteActualizadaIVA, ParameterName = "@ParteActualizadaIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RecargosIVA, ParameterName = "@RecargosIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MultaCorreccionIVA, ParameterName = "@MultaCorreccionIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CantidadPagadaIVA, ParameterName = "@CantidadPagadaIVA" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.TotalEfectivamentePagado, ParameterName = "@TotalEfectivamentePagado" });
                        cmd.ExecuteScalar();

                    }

                }
                else
                {
                    //actualizar Contraprestacion.TesofeActualizar
                    //llave NumeroOperacion y LlavePago
                    using (var cmd = _unitOfWork.CreateCommand())
                    {
                        cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_TesofeActualizar;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter { Value = entity.NumeroOperacion, ParameterName = "@NumeroOperacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RFC, ParameterName = "@RFC" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CURP, ParameterName = "@CURP" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RazonSocial, ParameterName = "@RazonSocial" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.FechaPresentacion, ParameterName = "@FechaPresentacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Sucursal, ParameterName = "@Sucursal" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.LlavePago, ParameterName = "@LlavePago" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Banco, ParameterName = "@Banco" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MedioRecepcion, ParameterName = "@MedioRecepcion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Dependencia, ParameterName = "@Dependencia" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Periodo, ParameterName = "@Periodo" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.SaldoFavor, ParameterName = "@SaldoFavor" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Importe, ParameterName = "@Importe" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ParteActualizada, ParameterName = "@ParteActualizada" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Recargos, ParameterName = "@Recargos" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MultaCorreccion, ParameterName = "@MultaCorreccion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Compensacion, ParameterName = "@Compensacion" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CantidadPagada, ParameterName = "@CantidadPagada" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ClaveReferenciaDPA, ParameterName = "@ClaveReferenciaDPA" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CadenaDependencia, ParameterName = "@CadenaDependencia" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ImporteIVA, ParameterName = "@ImporteIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.ParteActualizadaIVA, ParameterName = "@ParteActualizadaIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.RecargosIVA, ParameterName = "@RecargosIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.MultaCorreccionIVA, ParameterName = "@MultaCorreccionIVA", IsNullable = true });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.CantidadPagadaIVA, ParameterName = "@CantidadPagadaIVA" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.TotalEfectivamentePagado, ParameterName = "@TotalEfectivamentePagado" });
                        cmd.ExecuteScalar();
                    }
                }
            }

            return true;
        }

        public IEnumerable<EnteroTesofe> ObtenerPorCriterio(IPaging paging, EnteroTesofeCriterio criterio)
        {
            var result = new List<EnteroTesofe>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_TesofeObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = criterio.FechaPresentacionInicial, ParameterName = "@FechaPresentacionInicial", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.FechaPresentacionFinal, ParameterName = "@FechaPresentacionFinal", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.RazonSocial, ParameterName = "@RazonSocial", IsNullable = true });                
                cmd.Parameters.Add(new SqlParameter { Value = criterio.LlavePago, ParameterName = "@LlavePago", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.NumeroOperacion, ParameterName = "@NumeroOperacion", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.RFC, ParameterName = "@RFC", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.ClaveReferenciaDPA, ParameterName = "@ClaveReferenciaDPA", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.FechaCargaInicial, ParameterName = "@FechaCargaInicial", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = criterio.FechaCargaFinal, ParameterName = "@FechaCargaFinal", IsNullable = true });

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var enteroTesofe = LeerEnteroTesofe(reader);
                        result.Add(enteroTesofe);
                    }
                }
            }

            return result;
        }

        private static EnteroTesofe LeerEnteroTesofe(IDataReader reader)
        {
            var entity = new EnteroTesofe();
            entity.NumeroOperacion = Convert.ToInt64(reader["NumeroOperacion"]);
            entity.RFC = reader["RFC"].ToString();
            entity.CURP = reader["CURP"].ToString();
            entity.RazonSocial = reader["RazonSocial"].ToString();
            entity.FechaPresentacion = Convert.ToDateTime(reader["FechaPresentacion"]);
            entity.Sucursal = Convert.ToInt32(reader["Sucursal"]);
            entity.LlavePago = reader["LlavePago"].ToString();
            entity.Banco = reader["Banco"].ToString();
            entity.MedioRecepcion = reader["MedioRecepcion"].ToString();
            entity.Dependencia = reader["Dependencia"].ToString();
            entity.Periodo = reader["Periodo"].ToString();
            entity.SaldoFavor = Convert.ToDecimal(reader["SaldoFavor"]);
            entity.Importe = Convert.ToDecimal(reader["Importe"]);
            entity.ParteActualizada = Convert.ToDecimal(reader["ParteActualizada"]);
            entity.Recargos = Convert.ToDecimal(reader["Recargos"]);
            entity.MultaCorreccion = Convert.ToDecimal(reader["MultaCorreccion"]);
            entity.Compensacion = Convert.ToDecimal(reader["Compensacion"]);
            entity.CantidadPagada = Convert.ToDecimal(reader["CantidadPagada"]);
            entity.ClaveReferenciaDPA = Convert.ToInt32(reader["ClaveReferenciaDPA"]);
            entity.CadenaDependencia = reader["CadenaDependencia"].ToString();
            entity.ImporteIVA = Convert.ToDecimal(reader["ImporteIVA"].ToString());
            entity.ParteActualizadaIVA = reader["ParteActualizadaIVA"] != null ? Convert.ToDecimal(reader["ParteActualizadaIVA"]) : (decimal?)null;
            entity.RecargosIVA = reader["RecargosIVA"] != null ? Convert.ToDecimal(reader["RecargosIVA"]) : (decimal?)null;
            entity.MultaCorreccionIVA = reader["MultaCorreccionIVA"] != null ? Convert.ToDecimal(reader["MultaCorreccionIVA"]) : (decimal?)null;
            entity.CantidadPagadaIVA = Convert.ToDecimal(reader["CantidadPagadaIVA"]);
            entity.TotalEfectivamentePagado = Convert.ToDecimal(reader["TotalEfectivamentePagado"]);
            entity.FechaCarga = Convert.ToDateTime(reader["FechaCarga"]);

            return entity;
        }
        #endregion

        #region Interface IRepository

        public int Actualizar(EnteroTesofe entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(EnteroTesofe entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(EnteroTesofe entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EnteroTesofe> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EnteroTesofe> ObtenerPorCriterio(IPaging paging, EnteroTesofe entity)
        {
            throw new NotImplementedException();
        }

        public EnteroTesofe ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
