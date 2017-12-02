using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;


using System.Data;
using System.Data.SqlClient;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryCete : IRepository<Cete>
    {
        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        private int _pages { get; set; }


        #endregion

        #region Propiedades
        public int Pages { get { return _pages; } }

        #endregion

        #region Constructor

        public RepositoryCete(IUnitOfWork uow)
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

        #region Interface IRepository
        public int Actualizar(Cete entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(Cete entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(Cete entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cete> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cete> ObtenerPorCriterio(IPaging paging, Cete entity)
        {
            throw new NotImplementedException();
        }

        public Cete ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cetes"></param>
        /// <returns></returns>
        /// <remarks>
        /// No debe insertar duplicados basado en la fecha
        /// </remarks>
        public bool PersistExcelCetes(List<Cete> cetes)
        {
                
            foreach (Cete entity in cetes)
            {
                //valida que no exista el registro actual
               
                Cete cete = null;

                //buscar el registro actual
                using (var cmd = _unitOfWork.CreateCommand())
                {
                    cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_ObtenerPorFecha;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { Value = entity.Fecha, ParameterName = "@Fecha" });

                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read()) //solo regresa 1 registro si ya existe
                        {
                            cete = LeerCete(reader);  
                        }
                    }

                }

                if (cete == null)
                {
                    // insertar
                    using (var cmd = _unitOfWork.CreateCommand())
                    {
                                
                        cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_CetesInsertar;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter { Value = entity.Fecha, ParameterName = "@Fecha" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.TasaRendimiento, ParameterName = "@TasaRendimiento" });
                        cmd.ExecuteScalar();                          
                    }
                                    
                }
                else
                {
                    // actualizar fecha de rendimiento
                    using (var cmd = _unitOfWork.CreateCommand())
                    {

                        cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_CetesActualizar;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter { Value = cete.Identificador, ParameterName = "@IdCetes" });
                        cmd.Parameters.Add(new SqlParameter { Value = entity.TasaRendimiento, ParameterName = "@TasaRendimiento" });
                        cmd.ExecuteScalar();
                    }

                }  

            }
            
            return true;
        }

        public IEnumerable<Cete> ObtenerPorCriterio(IPaging paging, DateTime? fechaInicial, DateTime? fechaFinal)
        {
            var result = new List<Cete>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Contraprestacion.Contraprestacion_ObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = fechaInicial, ParameterName = "@FechaInicial", IsNullable = true });
                cmd.Parameters.Add(new SqlParameter { Value = fechaFinal, ParameterName = "@FechaFinal", IsNullable = true });               
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cete = LeerCete(reader);
                        result.Add(cete);
                    }
                }
                
            }

            return result;
        }

        private static Cete LeerCete(IDataReader reader)
        {
            var cete = new Cete
            {
                Identificador = Convert.ToInt32(reader["IdCetes"]),
                Fecha = Convert.ToDateTime(reader["Fecha"]),
                TasaRendimiento=Convert.ToDecimal(reader["TasaRendimiento"])
            
            };

            return cete;
        }
    }
}
