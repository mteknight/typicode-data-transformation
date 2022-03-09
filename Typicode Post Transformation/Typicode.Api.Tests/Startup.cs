using Microsoft.Extensions.DependencyInjection;

using Typicode.Api.Configurations;

namespace Typicode.Api.Tests
{
    public class Startup
    {
        public virtual void ConfigureServices(IServiceCollection services) => services.RegisterDependencies();
    }
}
