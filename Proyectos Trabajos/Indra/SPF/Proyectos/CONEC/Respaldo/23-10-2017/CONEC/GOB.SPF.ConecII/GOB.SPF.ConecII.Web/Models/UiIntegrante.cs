namespace GOB.SPF.ConecII.Web.Models
{
    using System;

    public class UiIntegrante : UiEntity
    {
        public Guid Identificador { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }
        public string CorreoPersonal { get; set; }
        public string IdArea { get; set; }
        public string Area { get; set; }
        public int IdJerarquia { get; set; }
        public string Jerarquia { get; set; }
        public int IdTipoReceptor { get; set; }
        public string TipoReceptor { get; set; }
        public int IdRol { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public bool IsActive = true;
        public bool EsCopia { get; set; }

        public string NombreCompleto => Nombre + " " + ApPaterno + " " + ApMaterno;
        public string CorreoTrabajo => !string.IsNullOrEmpty(Correo.Trim()) ? Correo.Trim() + "@cns.gob.mx" : "Sin Correo";
    }
}