namespace wsServices01800.Formiik
{
    /// <summary>
    /// Elemento afectado en la actualización flexible
    /// </summary>
    public class FlexibleField
    {
        private const string READONLY_ATRRIBUTE = "ReadOnly";
        private const string REQUIRED_ATRRIBUTE = "Requested";
        private const string VISIBLE_ATRRIBUTE = "Visible";

        /// <summary>
        /// Crea una instancia de la clase
        /// </summary>
        public FlexibleField()
        {
            this.Settings = new JsonDictionary<string, string>();
        }

        /// <summary>
        /// inicializa una instancia de <see cref="FlexibleField"/>
        /// </summary>
        /// <param name="fieldName">fieldName del campo en el formulario</param>
        /// <param name="isReadOnly">Indica si se debe cambiar el comportamiento del campo para que sea de solo lectura</param>
        /// <param name="isRequiered">Inidca si se debe cambiar el comportamiento del campo para que sea requerido</param>
        /// <param name="isVisible">Indica si se debe ocultar el campo</param>
        public FlexibleField(string fieldName, bool? isReadOnly = null, bool? isRequiered = null, bool? isVisible = null)
        {
            this.Name = fieldName;
            this.Settings = new JsonDictionary<string, string>();

            if(isReadOnly.HasValue)
            {
                this.Settings.Add(READONLY_ATRRIBUTE, isReadOnly.ToString());
            }

            if (isRequiered.HasValue)
            {
                this.Settings.Add(REQUIRED_ATRRIBUTE, isRequiered.ToString());
            }

            if (isVisible.HasValue)
            {
                this.Settings.Add(VISIBLE_ATRRIBUTE, isVisible.ToString());
            }
        }

        /// <summary>
        /// Identificador del campo (FieldName)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Settings para el campo
        /// </summary>
        public JsonDictionary<string, string> Settings { get; set; }
    }
}