using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Membership.Startup))]
namespace Membership
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
