using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPT373_AS2.Startup))]
namespace CPT373_AS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
