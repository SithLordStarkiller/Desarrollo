using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Core.Resources
{
    public static class Messages
    {
        public static string ValidateBase64 => "El archivo/documento no es valido.";
        public static string ValidateNameFile => "El nombre del archivo no puede ser vacio.";
        public static string ValidateExtension => "El nombre del archivo no es valido. Falta agregar la extensión.";
        public static string ValidateDirectory => "El valor de la Clasificación o Directorio no puede ir vacio.";

        public static string InsertSuccess => "Se registró el archivo satisfactoriamente.";
        public static string UpdateSuccess => "Se actualizó el archivo satisfactoriamente.";
        public static string FileSaveSuccess => "Se creó el archivo satisfactoriamente.";
        public static string FileSaveError => "Ocurrió un error al crear el archivo en el directorio FileTable.";
    }
}
