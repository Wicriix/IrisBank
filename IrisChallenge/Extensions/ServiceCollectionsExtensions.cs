using Iris.Commons.Settings;
using Iris.Data.DataContex;
using IrisChallenge.Interfaces;
using IrisChallenge.Services;
using Microsoft.EntityFrameworkCore;

namespace IrisChallenge.Extensions;

public static class ServiceCollectionsExtensions
{
    /// <summary>
    ///     Bind the json settings to classes. AddSettings method must be the first one invoked at startup
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GeneralSettings>(configuration.GetSection(nameof(GeneralSettings)));
    }

    /// <summary>
    ///     Add the data context
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IrisDbContext>(options =>
           options.UseSqlServer(
               configuration.GetConnectionString("IrisDbConnection"),
               sqlServerOptionsAction: sqlOpt =>
               {
                   sqlOpt.CommandTimeout(7200);
                   sqlOpt.EnableRetryOnFailure();
               })
               .EnableSensitiveDataLogging());
    }


    /// <summary>
    ///     Bind the json settings to classes. AddDependencies method must be the first one invoked at startup
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserService>();
        services.AddScoped<IAuthServices, AuthServices>(); 
        services.AddScoped<ITypeOfTaskService, TypeOfTaskService>();
        services.AddScoped<ITaskToDoService, TaskToDoService>(); 
    }

}