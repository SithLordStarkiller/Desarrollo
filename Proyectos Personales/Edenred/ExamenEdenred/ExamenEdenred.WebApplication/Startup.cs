using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExamenEdenred.WebApplication.Startup))]
namespace ExamenEdenred.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
