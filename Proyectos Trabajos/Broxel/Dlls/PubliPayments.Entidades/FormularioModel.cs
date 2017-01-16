using System;
using System.Collections.Generic;

namespace PubliPayments.Entidades
{
   public class FormularioModel
    {
        public int IdFormulario { get; set; }
        public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public string Estatus { get; set; }
        public string FechaAlta { get; set; }
        public int Captura { get; set; }
        public string Ruta { get; set; }
        public string Error { get; set; }
        public List<SubFormularioModel> ListaSubFormularios = new List<SubFormularioModel>();
        public List<FuncionJavascriptModel> FuncionesJavascripts = new List<FuncionJavascriptModel>();

        public FormularioModel()
        { }

        public FormularioModel(string idFormulario, string idAplicacion, string nombre, string descripcion, string version, string estatus, string fechaAlta, string captura, string ruta)
        {
            IdFormulario = idFormulario != null ? Convert.ToInt32(idFormulario) : -1;
            IdAplicacion = idAplicacion != null ? Convert.ToInt32(idAplicacion) : -1;
            Nombre = nombre ?? "";
            Descripcion = descripcion ?? "";
            Version = version ?? "";
            Estatus = estatus ?? "";
            Captura = captura != null ? Convert.ToInt32(captura) : -1;
            FechaAlta = fechaAlta ?? "";
            Ruta = ruta ?? "";
        }
    }
}
