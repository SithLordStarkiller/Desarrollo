using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValidadoresMVC.Startup))]
namespace ValidadoresMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
