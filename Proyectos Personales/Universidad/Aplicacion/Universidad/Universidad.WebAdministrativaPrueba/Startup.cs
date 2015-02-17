using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Universidad.WebAdministrativaPrueba.Startup))]
namespace Universidad.WebAdministrativaPrueba
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
