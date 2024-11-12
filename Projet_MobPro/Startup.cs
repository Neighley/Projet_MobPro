using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet_MobPro.Startup))]
namespace Projet_MobPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
