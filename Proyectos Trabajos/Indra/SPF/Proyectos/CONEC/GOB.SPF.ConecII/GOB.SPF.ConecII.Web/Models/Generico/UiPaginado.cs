using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models.Generico
{
    public class UiPaging: IPaging
    {
        public bool All { get; set; }

        public int CurrentPage { get; set; }

        public int Pages { get; set; }

        public int Rows { get; set; }
    }
}