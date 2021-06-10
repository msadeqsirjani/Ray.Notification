using Microsoft.Owin.Cors;
using Owin;

namespace Ray.Notification.Service
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
