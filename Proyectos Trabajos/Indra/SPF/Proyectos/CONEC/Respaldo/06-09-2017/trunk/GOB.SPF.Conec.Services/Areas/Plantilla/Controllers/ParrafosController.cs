using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Business.Plantilla;
using GOB.SPF.ConecII.Entities.DTO;

namespace Cns.Sies.Rest.Mvc.Areas.Plantilla.Controllers
 {
     public class ParrafosController : ApiController
     {
 
         ParrafosBusiness mAdministraParrafos;
 
         public ParrafosController()
         {
             mAdministraParrafos = new ParrafosBusiness();
         }
 
 
         
     }
 }
