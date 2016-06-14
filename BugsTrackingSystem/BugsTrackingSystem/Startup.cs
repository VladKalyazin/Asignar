using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugsTrackingSystem.Startup))]
namespace BugsTrackingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
