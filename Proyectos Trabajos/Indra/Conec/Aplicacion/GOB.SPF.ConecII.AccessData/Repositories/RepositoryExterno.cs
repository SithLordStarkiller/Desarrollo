using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryExterno : Repository<Externo>
    {
        public RepositoryExterno(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// Inserta en la Bd un nuevo externo (contacto o solicitante)
        /// </summary>
        /// <param name="entity">Cliente o soliciante a guardar.</param>
        /// <param name="cliente">Cliente del contacto o solicitante</param>
        /// <returns></returns>
        public int Insertar(Externo entity, int cliente)
        {
            using (var cmd = UoW.CreateCommand())
            {
                cmd.CommandText = Solicitud.ExternoInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = cliente, ParameterName = "@IdCliente" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.IdTipoPersona, ParameterName = "@IdTipoPersona" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.TipoContacto.Identificador, ParameterName = "@IdTipoContacto" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Nombre, ParameterName = "@Nombre" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoPaterno, ParameterName = "@ApellidoPaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.ApellidoMaterno, ParameterName = "@ApellidoMaterno" });
                cmd.Parameters.Add(new SqlParameter() { Value = entity.Cargo, ParameterName = "@Cargo" });
                var result = (int)cmd.ExecuteScalar();
                return result;
            }
        }

        

    }
}
