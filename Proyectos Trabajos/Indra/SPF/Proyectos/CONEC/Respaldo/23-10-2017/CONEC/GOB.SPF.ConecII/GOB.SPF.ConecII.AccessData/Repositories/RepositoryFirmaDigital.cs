using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using GOB.SPF.ConecII.Entities;

namespace GOB.SPF.ConecII.AccessData.Repositories
{
    public class RepositoryFirmaDigital : Repository<FirmaDigital>
    {
        #region Variables privadas

        private readonly UnitOfWorkCatalog _unitOfWork;

        #endregion

        #region Constructor

        public RepositoryFirmaDigital(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("No existe una unidad de trabajo");

            _unitOfWork = uow as UnitOfWorkCatalog;
            if (_unitOfWork == null)
            {
                throw new NotSupportedException("La unidad de trabajo esta nulo.");
            }
        }

        public int AgregarCertificadoEmisor(byte[] file)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Firma.Firma_CertificadoEmisorInsertar;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter() { Value = file, ParameterName = "@Documento" });
                return cmd.ExecuteNonQuery();
            }            
        }

        #endregion

        #region Public Methods
        public List<byte[]> ObtenerEmisores()
        {
            var result = new List<byte[]>();

            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = Schemas.Firma.Firma_CertificadoEmisorObtenerTodos;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var documento = (byte[])reader["Documento"];
                        result.Add(documento);
                    }
                }
            }

            return result;
        }

        #endregion

    }
}
