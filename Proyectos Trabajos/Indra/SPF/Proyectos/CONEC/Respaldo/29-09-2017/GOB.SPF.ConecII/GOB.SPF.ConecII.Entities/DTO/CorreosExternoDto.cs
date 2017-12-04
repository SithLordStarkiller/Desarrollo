using System.Collections.Generic;
using System.Data;
using Microsoft.SqlServer.Server;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class CorreosExternoDto : List<Externo>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                        new SqlMetaData("IdCorreo", SqlDbType.Int),
                        new SqlMetaData("IdExterno", SqlDbType.Int),
                        new SqlMetaData("Correo", SqlDbType.VarChar, 50),
                        new SqlMetaData("Activo", SqlDbType.Bit)
                    );

            foreach (var externo in this)
            {
                foreach (var correos in externo.Correos)
                {
                    sqlRow.SetInt32(0, correos.Identificador);
                    sqlRow.SetInt32(1, externo.Identificador);
                    sqlRow.SetSqlString(2, correos.CorreoElectronico);
                    sqlRow.SetSqlBoolean(3, correos.Activo);
                    yield return sqlRow;
                }

            }

        }


    }
}
