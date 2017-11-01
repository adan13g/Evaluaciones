using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mysql_Asp.Startup))]
namespace Mysql_Asp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
