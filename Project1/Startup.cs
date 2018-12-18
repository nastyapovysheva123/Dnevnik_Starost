using Microsoft.Owin;
using Owin;
using Project1.Migrations;

[assembly: OwinStartupAttribute(typeof(Project1.Startup))]
namespace Project1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new SQLiteMigration().CreateDB();
        }
    }
}
