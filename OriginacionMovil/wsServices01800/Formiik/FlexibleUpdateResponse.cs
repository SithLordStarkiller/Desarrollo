using System.Collections.Generic;

namespace wsServices01800.Formiik
{
    public class FlexibleUpdateResponse
    {
        public FlexibleUpdateResponse()
        {
            this.UpdateFieldsValues = new JsonDictionary<string,string>();

            this.AfectedFields = new List<FlexibleField>();

            this.FormiikReservedWords = new List<FlexibleUpdateReservedWords>();
        }

        /// <summary>
        /// Listado de campos de los que debe actualizar el valor Key = KeyForSave, Value = Nuevo valor
        /// </summary>
        public JsonDictionary<string,string> UpdateFieldsValues { get; set; }

        /// <summary>
        /// Elementos de los que debe modificar el comportamiento (la cantidad de elementos son opcionales, FieldName)
        /// </summary>
        public List<FlexibleField> AfectedFields { get; set; }

        /// <summary>
        /// Palabras reservadas de Formiik
        /// </summary>
        public List<FlexibleUpdateReservedWords> FormiikReservedWords { get; set; }
    }
}