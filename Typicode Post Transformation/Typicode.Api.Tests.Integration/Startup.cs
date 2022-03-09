using Microsoft.Extensions.DependencyInjection;

using Typicode.Api.Configurations;

namespace Typicode.Api.Tests.Integration
{
    public class Startup : Tests.Startup
    {
        public override void ConfigureServices(IServiceCollection services) => services.RegisterDependencies();
    }
}
