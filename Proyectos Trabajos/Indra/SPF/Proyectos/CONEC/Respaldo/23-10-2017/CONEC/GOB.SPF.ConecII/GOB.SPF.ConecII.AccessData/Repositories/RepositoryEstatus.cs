using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryEstatus : Repository<Estatus>
    {
        #region Constructor

        public RepositoryEstatus(IUnitOfWork uow) : base(uow)
        {
        }

        #endregion


        public List<Estatus> ObtenerPorCriterio(int identificadorNegocio)
        {
            var estatusList = new List<Estatus>();
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Schemas.Catalogos.EstatusObtenerPorCriterio;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { Value = identificadorNegocio, ParameterName = "@IdEntidadNegocio" });
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var estatus = new Estatus
                        {
                            Identificador = Convert.ToInt32(reader["IdEstatus"]),
                            EntidadNegocio = new EntidadNegocio {Identificador = reader["IdEntidadNegocio"] as int? ?? 0},
                            Nombre = reader["Descripcion"].ToString()
                        };
                        estatusList.Add(estatus);
                    }
                }
                return estatusList;
            }

        }
    }
}
