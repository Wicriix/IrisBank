using FluentValidation.AspNetCore;
using Iris.Commons.Library.Middelwares;
using Iris.Commons.Settings;
using Iris.Commons.Storage.Repository;
using Iris.Data.Repository;
using Iris.Common.Extensions;
using IrisChallenge.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Iris.Shared.Mappers;

namespace IrisChallenge
{
    public class Startup
    {
        private readonly GeneralSettings _generalSettings;
        private WebApplication app;
        public Startup(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;
            _generalSettings = configuration.GetSection(nameof(GeneralSettings)).Get<GeneralSettings>();
            ConfigureServices(builder);
            app = builder.Build();
            Configure(app);
        }
        public WebApplication GetApp()
        {
            return app;
        }

        public void ConfigureServices(WebApplicationBuilder builder)
        {
            ConfigureDatabaseServices(builder.Services, builder.Configuration);
            ConfigureRepositories(builder.Services);
            builder.Services.AddDependencies();
            builder.Services.AddSettings(builder.Configuration);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_generalSettings.SecretKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add services to the container.
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(_generalSettings.MajorVersion, _generalSettings.MinorVersion);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            builder.Services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Add CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
            builder.Services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }


        public void Configure(WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_generalSettings.ApiName} v1");
                    c.OAuthAppName(_generalSettings.ApiName);
                });
            }

            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors("ApiCorsPolicy");

            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.MapControllers();

        }

        protected virtual void ConfigureDatabaseServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataContext(configuration);
        }

        protected void ConfigureRepositories(IServiceCollection services)
        {
            services.AddApplicationRepositories<IUserRepository>(typeof(BaseSqlRepository<>));
        }

    }
}
