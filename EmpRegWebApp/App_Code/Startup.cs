using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmpRegWebApp.Startup))]
namespace EmpRegWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
