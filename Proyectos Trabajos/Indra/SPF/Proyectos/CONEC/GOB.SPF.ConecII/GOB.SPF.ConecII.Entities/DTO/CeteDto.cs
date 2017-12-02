using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class CeteDto : List<Cete>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(
                           new SqlMetaData("IdCetes", SqlDbType.Int),
                           new SqlMetaData("Fecha", SqlDbType.DateTime),
                           new SqlMetaData("TasaRendimiento", SqlDbType.Decimal,18,9)
                       );

            foreach (var cete in this)
            {
                sqlRow.SetInt32(0, cete.Identificador);
                sqlRow.SetDateTime(1, cete.Fecha);
                sqlRow.SetDecimal(2, cete.TasaRendimiento);
                yield return sqlRow;
            }
        }
    }
}
