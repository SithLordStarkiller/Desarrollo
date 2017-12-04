using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Web.Controllers;
using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla.Controllers
{
    public class TiposDocumentoController : BaseController<Entities.Plantilla.UiTiposDocumento>
    {
        
        public TiposDocumentoController():base("TiposDocumento")
        {
            
        }
        
    }
}