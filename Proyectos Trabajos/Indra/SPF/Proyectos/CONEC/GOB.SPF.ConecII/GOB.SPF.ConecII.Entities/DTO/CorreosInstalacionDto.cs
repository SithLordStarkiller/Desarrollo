using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class CorreosInstalacionDto : List<Correo>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                new SqlMetaData("IdCorreoInstalacion", SqlDbType.Int),
                new SqlMetaData("IdInstalacion", SqlDbType.Int),
                new SqlMetaData("Correo", SqlDbType.VarChar, 50),
                new SqlMetaData("Activo", SqlDbType.Bit)
            );

            foreach (var instalacion in this)
            {
                sqlRow.SetInt32(0, instalacion.Identificador);
                sqlRow.SetInt32(1, instalacion.UsuarioEntity.Identificador);
                sqlRow.SetSqlString(2, instalacion.CorreoElectronico);
                sqlRow.SetSqlBoolean(3, instalacion.Activo);
                yield return sqlRow;
            }
        }
    }
}
