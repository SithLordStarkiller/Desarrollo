namespace Mcs.Core.Exceptions
{
    public static class Messages
    {
        public static string ConfigurationExceptionFind => "No se encontró el nombre de conexion llamado {0} en el app/web.config.";
        public static string ConfigurationExceptionCreate => "Falló al crear la conexion usando el connection string llamado {0} en el app/web.config.";
        public static string InvalidSaveChangesDoubleException => "Error al guardar. No se puede guardar los cambios dos veces.";
        public static string NullParameterException => "El/los valor(es) de los parametro(s) no puede(n) ser nulo(s).";

        #region Respuestas Busqueda

        public static string NotFoundDataByFilters => "No se encontraron registros con los criterios de busqueda.";

        public static string NotFoundData => "No se encontraron registros.";

        #endregion
    }
}
