using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripAgency.Startup))]
namespace TripAgency
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
