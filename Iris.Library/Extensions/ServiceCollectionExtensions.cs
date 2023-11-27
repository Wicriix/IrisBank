using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Iris.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped(s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User);

        }

        public static void AddApplicationRepositories<T>(this IServiceCollection services, Type baseImplementation)
            where T : class
        {
            services.Scan(scan => scan.FromAssemblyOf<T>()
                .AddClasses(classes => classes.AssignableTo(baseImplementation))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}