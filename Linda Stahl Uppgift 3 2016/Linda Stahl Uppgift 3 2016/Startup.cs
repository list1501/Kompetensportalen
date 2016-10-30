using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Linda_Stahl_Uppgift_3_2016.Startup))]
namespace Linda_Stahl_Uppgift_3_2016
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
