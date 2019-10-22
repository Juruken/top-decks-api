using System.Reflection;
using Autofac;
using TopDecks.Api.Core.Data;
using Module = Autofac.Module;

namespace TopDecks.Api.Core.AutofacModules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IGetDecksQuery).GetTypeInfo().Assembly)
                .InNamespaceOf<IGetDecksQuery>()
                .AsImplementedInterfaces();
        }
    }
}
