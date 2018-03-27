using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASFront.Startup))]
namespace ASFront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
