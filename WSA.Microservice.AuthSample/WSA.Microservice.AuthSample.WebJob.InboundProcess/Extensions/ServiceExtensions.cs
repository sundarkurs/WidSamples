using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WSA.Microservice.AuthSample.WebJob.InboundProcess.Interfaces;

namespace WSA.Microservice.AuthSample.WebJob.InboundProcess.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITodoImporter, Services.TodoImporter>();
        }

        public static void AddConfigurations(this IServiceCollection services, HostBuilderContext context)
        {

        }
    }
}
