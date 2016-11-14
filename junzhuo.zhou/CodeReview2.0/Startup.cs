using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeReview.Startup))]
namespace CodeReview
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
