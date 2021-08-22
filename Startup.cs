using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(websitee.Startup))]
namespace websitee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
