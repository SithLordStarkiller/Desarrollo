using System;
using Microsoft.Owin;
using Owin;
using Universidad.WebAdministrativaPrueba;

[assembly: OwinStartup("ProductionConfiguration", typeof(Startup.ProductionStartup2))]
namespace Universidad.WebAdministrativaPrueba
{
    public partial class Startup {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                var t = DateTime.Now.Millisecond.ToString();
                return context.Response.WriteAsync(t + " Production OWIN App");
            });
        }

        public class ProductionStartup2
        {
            public void Configuration(IAppBuilder app)
            {
                app.Run(context =>
                {
                    string t = DateTime.Now.Millisecond.ToString();
                    return context.Response.WriteAsync(t + " 2nd Production OWIN App");
                });
            }
        }
    }
}
