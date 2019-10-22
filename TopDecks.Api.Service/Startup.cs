using System.Runtime.Remoting.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Serilog;

namespace TopDecks.Api.Service
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            app.Use(async (httpContext, next) =>
            {
                httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
                httpContext.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Origin,X-Requested-With,X-Session-Key,X-Admin-Key,X-Developer-Key,X-Api-Key,Content-Type,Accept,Authorization,Accept-Encoding");
                httpContext.Response.Headers.Add("Access-Control-Expose-Headers",
                    "X-Api-Key,Content-Type,Content-Encoding,Content-Length,ETag,Location");

                NancyContextSink.Current = new NancyContextSink(httpContext.TraceIdentifier);
                try
                {
                    await next();
                }
                finally
                {
                    CallContext.FreeNamedDataSlot("NancyContext");
                }
            });
            app.UseOwin(x => x.UseNancy(new NancyOptions
            {
                Bootstrapper = new Bootstrapper()
            }));
            loggerFactory.AddSerilog();
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}
