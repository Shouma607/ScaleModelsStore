using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScaleModelsStore.Startup))]
namespace ScaleModelsStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
