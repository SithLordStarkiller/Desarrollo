using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Universidad.WebUsuarios.Startup))]
namespace Universidad.WebUsuarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
