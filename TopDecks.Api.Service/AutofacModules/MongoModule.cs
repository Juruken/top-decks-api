using Autofac;
using Microsoft.Extensions.Configuration;
using TopDecks.Api.Core.Mongo;

namespace TopDecks.Api.Service.AutofacModules
{
    public class MongoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => MongoDatabaseConfigurator.Configure(c.Resolve<IConfigurationRoot>()["mongo"]))
                .SingleInstance();
        }
    }
}
