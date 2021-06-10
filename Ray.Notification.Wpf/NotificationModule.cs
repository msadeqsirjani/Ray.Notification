using Autofac;
using Ray.Notification.Common.Services.DatabaseService;
using Ray.Notification.Common.Services.WebConnector;

namespace Ray.Notification.Wpf
{
    public class NotificationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebConnectorService>().As<IWebConnectorService>().InstancePerLifetimeScope();
            builder.RegisterType(typeof(DatabaseService)).SingleInstance();
        }
    }
}
