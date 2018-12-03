using Autofac;
using MessageService;
using MessageService.Repositories;
using MessageService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MessageDbContext>().AsSelf();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<MessageWcfService>().As<IMessageWcfService>();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>();
            builder.RegisterType<MessageRequestValidator>().AsSelf();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
