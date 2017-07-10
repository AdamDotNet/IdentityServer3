using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using MinimalServer.Config;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(MinimalServer.Startup))]

namespace MinimalServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .CreateLogger();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(Users.Get())
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get());
            factory.ViewService = new Registration<IViewService>(typeof(MvcViewService));

            var options = new IdentityServerOptions
            {
                SigningCertificate = Certificate.Load(),
                Factory = factory,
                RequireSsl = false
            };

            app.Map("/Identity", identityApp =>
            {
                identityApp.UseIdentityServer(options);
            });
        }
    }
}
