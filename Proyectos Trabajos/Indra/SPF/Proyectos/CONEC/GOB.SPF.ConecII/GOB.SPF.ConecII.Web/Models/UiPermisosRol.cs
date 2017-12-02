namespace GOB.SPF.ConecII.Web.Models
{
    public class UiPermisosRol : UiEntity
    {
        public UiPermisosRol()
        {
            Area = new UiArea();
            Menu = new UiMenu();
            Rol = new UiRol();
        }

        public int Identificador { get; set; }
        public UiArea Area { get; set; }
        public UiRol Rol { get; set; }
        public UiMenu Menu { get; set; }
    }
}