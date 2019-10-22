using System;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace TopDecks.Api.Service.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("settings.json")
                .Build()).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => c.Resolve<IConfiguration>());
        }
    }
}
