using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AkalTrucking.Startup))]
namespace AkalTrucking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
