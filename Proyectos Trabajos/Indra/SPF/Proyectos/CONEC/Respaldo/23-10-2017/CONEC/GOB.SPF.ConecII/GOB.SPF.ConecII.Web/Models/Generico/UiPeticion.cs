using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models.Generico
{
    public class UiPeticion<TObj> : IPeticion<TObj>
    {
        public IPaging Paginado { get; set; }

        public TObj Solicitud { get; set; }

        public string Usuario { get; set; }
    }
}