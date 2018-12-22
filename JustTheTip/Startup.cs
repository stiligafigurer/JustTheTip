using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JustTheTip.Startup))]
namespace JustTheTip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
