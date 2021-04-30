using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PGReservation.Startup))]
namespace PGReservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
