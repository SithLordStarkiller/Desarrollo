using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamenCsi.Site.Startup))]
namespace ExamenCsi.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
