using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using GOB.SPF.ConecII.Amatzin.Core.Interfaces;
using GOB.SPF.ConecII.Amatzin.Entities;
using GOB.SPF.ConecII.Amatzin.AccessData.Repositories;
using GOB.SPF.ConecII.Amatzin.Entities.Request;
using GOB.SPF.ConecII.Amatzin.Core.Extensions;
using GOB.SPF.ConecII.Amatzin.Core.Resources;

namespace GOB.SPF.ConecII.Amatzin.Business
{
    public class Archivos : IRepository<Archivo>
    {
        public Result<Archivo> Actualizar(Archivo entity)
        {
            throw new NotImplementedException();
        }

        public Result<Archivo> ConsultarHistoricoPorId(object id)
        {
            return new ArchivoRepository().ConsultarHistoricoPorId(id);
        }

        public Result<Archivo> Insertar(Archivo entity)
        {
            try
            {
                // Validación del archivo a insertar.
                if (!entity.Base64.IsBase64()) throw new Exception(Messages.ValidateBase64);
                // Validación del nombre del archivo.
                if (string.IsNullOrEmpty(entity.Nombre)) throw new Exception(Messages.ValidateNameFile);
                // Validación del nombre de archivo que contenga una extensión.
                if (!Path.HasExtension(entity.Nombre)) throw new Exception(Messages.ValidateExtension);
                // Validación del Directorio donde se almacenara el archivo.
                if(string.IsNullOrEmpty(entity.Directorio)) throw new Exception(Messages.ValidateDirectory);

                DirectoryStorage dir = new DirectoryStorage();
                dir.ValidateDirectory(entity.Directorio);

                entity.NombreSistema = Guid.NewGuid().ToString()+Path.GetExtension(entity.Nombre);
                byte[] decodedBytes = Convert.FromBase64String(entity.Base64);
                var result = dir.CreateFile(entity.NombreSistema, entity.Directorio, decodedBytes);
                //var result = new FileTableRepository().Save(entity.NombreSistema, decodedBytes, "/" + entity.Directorio);
                if (!result.Success)
                {
                    return new Result<Archivo>()
                    {
                        Success = false,
                        Message = Messages.FileSaveError
                    };
                }

                entity.ArchivoIdParent = (entity.ArchivoId == 0 ? 0 : entity.ArchivoId);

                //byte[] decodedBytes = Convert.FromBase64String(entity.Base64);
                entity.Extension = Path.GetExtension(entity.Nombre).Replace(".","").ToLower();
                entity.FechaRegistro = DateTime.Now;
                
                entity.Stream = new TableStorage()
                {
                    StreamId = result.Entity.StreamId
                };
                return new ArchivoRepository().Insertar(entity);
            }
            catch (Exception ex)
            {
                return new Result<Archivo>()
                {
                   Success = false,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(), Message = ex.Message } }
                };
            }
            
        }


        public Result<Archivo> ObtenerPorId(object id)
        {
            return new ArchivoRepository().ObtenerPorId(id);
        }

        public Result<Archivo> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
