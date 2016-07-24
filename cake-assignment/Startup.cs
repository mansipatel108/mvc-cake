using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cake_assignment.Startup))]
namespace cake_assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
