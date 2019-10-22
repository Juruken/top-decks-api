using Autofac;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Nancy.Extensions;
using Newtonsoft.Json;
using Serilog;
using TopDecks.Api.Core.AutofacModules;
using TopDecks.Api.Service.AutofacModules;

namespace TopDecks.Api.Service
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            environment.Views(runtimeViewUpdates: true);
            base.Configure(environment);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            ConfigureCallContext(pipelines);
            ConfigureErrorHandling(pipelines);
        }

        private static void ConfigureCallContext(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(context =>
            {
                if (NancyContextSink.Current == null) return null;

                NancyContextSink.Current.NancyContext = context;

                return null;
            });
        }

        private static void ConfigureErrorHandling(IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((context, ex) =>
            {
                Log.Error(ex, "An error occured processing the request.");

                Response response = HttpStatusCode.InternalServerError;

                return response;
            });
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(
                typeof(MongoModule).GetAssembly(),
                typeof(DataModule).GetAssembly());

            builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>();

            var container = builder.Build();
            return container;
        }
    }
}
