using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class TelefonosExternosDto : List<Externo>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                new SqlMetaData("IdTelefono", SqlDbType.Int),
                new SqlMetaData("IdExterno", SqlDbType.Int),
                new SqlMetaData("IdTipoTelefono", SqlDbType.Int),
                new SqlMetaData("Telefono", SqlDbType.VarChar, 50),
                new SqlMetaData("Extension", SqlDbType.VarChar, 50),
                new SqlMetaData("Activo", SqlDbType.Bit)
            );

            foreach (var externo in this)
            {
                foreach (var telefonos in externo.Telefonos)
                {
                    sqlRow.SetInt32(0, telefonos.Identificador);
                    sqlRow.SetInt32(1, externo.Identificador);
                    sqlRow.SetInt32(2, telefonos.TipoTelefono.Identificador);
                    sqlRow.SetSqlString(3, telefonos.Numero);
                    sqlRow.SetSqlString(4, telefonos.Extension);
                    sqlRow.SetSqlBoolean(5, telefonos.Activo);
                    yield return sqlRow;
                }
            }
        }
    }
}
