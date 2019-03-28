using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Project_Try1.Startup))]

namespace Project_Try1 {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}