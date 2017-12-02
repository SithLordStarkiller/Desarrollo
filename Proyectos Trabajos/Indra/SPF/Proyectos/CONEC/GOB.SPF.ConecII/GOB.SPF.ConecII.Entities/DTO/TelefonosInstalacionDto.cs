using System.Collections.Generic;
using System.Data;
using Microsoft.SqlServer.Server;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class TelefonosInstalacionDto :List<Telefono>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                new SqlMetaData("IdTelefonoInstalacion", SqlDbType.Int),
                new SqlMetaData("IdInstalacion", SqlDbType.Int),
                new SqlMetaData("IdTipoTelefono", SqlDbType.Int),
                new SqlMetaData("Telefono", SqlDbType.VarChar, 50),
                new SqlMetaData("Extension", SqlDbType.VarChar, 50),
                new SqlMetaData("Activo", SqlDbType.Bit)
            );

            foreach (var instalacion in this)
            {
                sqlRow.SetInt32(0,instalacion.Identificador);
                sqlRow.SetInt32(1, instalacion.UsuarioEntity.Identificador);
                sqlRow.SetInt32(2, instalacion.TipoTelefono.Identificador);
                sqlRow.SetSqlString(3, instalacion.Numero);
                sqlRow.SetSqlString(4, instalacion.Extension);
                sqlRow.SetSqlBoolean(5, instalacion.Activo);
                yield return sqlRow;
            }
        }
    }
}
