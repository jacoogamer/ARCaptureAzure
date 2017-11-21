using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ARRetailManager.Startup))]
namespace ARRetailManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
