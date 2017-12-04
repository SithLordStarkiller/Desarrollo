using System.Collections.Generic;
using System.Data;
using Microsoft.SqlServer.Server;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class DocumentosClienteDto : List<ClientesDocumentos>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sqlRow = new SqlDataRecord(new SqlMetaData("IdClienteDocumento", SqlDbType.Int),
                new SqlMetaData("IdCliente", SqlDbType.Int),
                //Tipo documento
                new SqlMetaData("IdTipoDocumento", SqlDbType.Int),
                // Identificador del documento en Amatzin
                new SqlMetaData("DocumentoSoporte", SqlDbType.Int),
                // Borrado lógico
                new SqlMetaData("Activo", SqlDbType.Bit)
            );

            foreach (var documento in this)
            {
                sqlRow.SetInt32(0, documento.Identificador);
                if (documento.Cliente == null)
                    sqlRow.SetDBNull(1);
                else
                    sqlRow.SetInt32(1, documento.Cliente.Identificador);
                sqlRow.SetInt32(2, 1);// documento.TipoDocumento.Identificador);
                sqlRow.SetInt32(3, documento.ArchivoId);
                sqlRow.SetSqlBoolean(4, documento.Activo);
                yield return sqlRow;
            }

        }


    }
}
