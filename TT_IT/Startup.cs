using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TT_IT.Startup))]
namespace TT_IT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
