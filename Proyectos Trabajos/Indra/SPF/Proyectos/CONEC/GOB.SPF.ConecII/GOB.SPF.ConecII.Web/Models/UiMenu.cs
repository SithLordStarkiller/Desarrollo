using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiMenu
    {
        public UiMenu()
        {
            SubMenus = new List<UiMenu>();
        }

        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public List<UiMenu> SubMenus { get; set; }
    }
}