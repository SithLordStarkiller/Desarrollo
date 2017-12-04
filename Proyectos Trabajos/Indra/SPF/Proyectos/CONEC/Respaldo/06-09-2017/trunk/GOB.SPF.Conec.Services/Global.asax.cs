using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace GOB.SPF.Conec.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SwaggerConfig.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
