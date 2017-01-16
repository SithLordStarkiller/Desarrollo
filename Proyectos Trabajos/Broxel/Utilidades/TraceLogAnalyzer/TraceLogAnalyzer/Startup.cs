using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TraceLogAnalyzer.Startup))]
namespace TraceLogAnalyzer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
