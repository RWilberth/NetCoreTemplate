using CoreApp.Application.Configurations;
using CoreApp.Application.Interfaces;
using CoreApp.Infrastructure.Configurations;
using CoreApp.Persistence.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddCoreApplication();
            services.AddAllHandlers();
            // COdigo para agregar validacion de seguridad jwt
            // services.AddJwtSecurity(Configuration.GetValue<string>("Jwt:Key"));
            services.AddCoreInfraestructure();
            services.AddDatabaseInfraestructure<IUnitOfWork, ProjectDBContext>(Configuration.GetConnectionString("ProjectsDBPostgresConection"), DataBases.Postgres);
           services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExtensionsApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string swagerJson = "./v1/swagger.json";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                swagerJson = "/swagger/v1/swagger.json";
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(swagerJson, "ExtensionsApi v1");
            });
            app.ConfigureExceptionHandler();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
