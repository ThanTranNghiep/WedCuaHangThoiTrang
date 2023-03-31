using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KNT_Shop.Startup))]
namespace KNT_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
