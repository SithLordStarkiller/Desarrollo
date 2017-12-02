using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using GOB.SPF.ConecII.Amatzin.Entities;
using GOB.SPF.ConecII.Amatzin.Core.Resources;

using GOB.SPF.ConecII.Amatzin.Core.Configuration;

namespace GOB.SPF.ConecII.Amatzin.Business
{
    class DirectoryStorage
    {

        private readonly string _directoryStoragePath = AppSettingsConfig.GetValueDirectoryStorage();

        public bool ValidateDirectory(string path)
        {
            if(string.IsNullOrEmpty(path)) throw new Exception("El Directorio o Clasificación no puede ser vacio.");
            if(!IsValidPath(string.Concat(_directoryStoragePath,path))) throw new Exception("El Directorio o Clasificación no es valido.");

            try
            {
                ExistsDirectory(string.Concat(_directoryStoragePath, path));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el directorio. Mensaje: " + ex.Message);
            }
        }

        private bool IsValidPath(string path)
        {
            if(path.Contains("/")) path = path.Replace("/",@"\");
            //Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
            //if (!driveCheck.IsMatch(path.Substring(0, 3))) return false;
            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            Regex valid = new Regex(@"^(?:[\w]\:|\\)(\\|\\\\[a-z_\-\s0-9\.]+)");
            //if (containsABadCharacter.IsMatch(path.Substring(3, path.Length)))
            if (!valid.IsMatch(path))
                return false;

            return true;
        }

        private void ExistsDirectory(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(path));
                if (!dir.Exists) dir.Create();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el directorio. Mensaje: " + ex.Message);
            }
        }

        public Result<TableStorage> CreateFile(string name, string path, byte[] file)
        {
            try
            {
                File.WriteAllBytes( string.Concat(_directoryStoragePath, path),file);
                File.Create(Path.Combine(path,name),file.Length,FileOptions.WriteThrough,new System.Security.AccessControl.FileSecurity(,System.Security.AccessControl.AccessControlSections.Access))
                return new Result<TableStorage>
                {
                    Success = true,
                    Message = Messages.FileSaveSuccess
                };
            }
            catch (Exception ex)
            {
                return new Result<TableStorage>
                {
                    Success = false,
                    Message = Messages.FileSaveError,
                    Errors = new List<Error> { new Error { Code = ex.GetHashCode(), Message = ex.Message } }
                };
            }
        }
    }
}
