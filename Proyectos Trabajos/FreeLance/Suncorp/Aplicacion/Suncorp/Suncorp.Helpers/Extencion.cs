namespace Suncorp.Helpers
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static List<T> ToListModel<T>(this T entidad) where T : class
        {
            try
            {
                var listEntities = new List<T>();

                return listEntities;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<T> ToList<T>(this object dataTable) where T : new()
        {
            var list = new List<T>();

            return list;
        }
    }
}
