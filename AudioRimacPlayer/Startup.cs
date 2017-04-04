using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AudioRimacPlayer.Startup))]
namespace AudioRimacPlayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
