using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GOB.SPF.ConecII.Web.Startup))]
namespace GOB.SPF.ConecII.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
