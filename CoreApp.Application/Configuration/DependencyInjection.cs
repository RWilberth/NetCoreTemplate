using CoreApp.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;
using System.Reflection;
using System.Text;

namespace CoreApp.Application.Configurations
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddCoreApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return serviceCollection;
        }

        public static IServiceCollection AddOrderApplication(this IServiceCollection serviceCollection)
        {
            MediatRServiceConfiguration serviceConfig = new MediatRServiceConfiguration();
            ServiceRegistrar.AddRequiredServices(serviceCollection, serviceConfig);
            serviceCollection.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                 .Where(c =>
                 {
                     bool isExtensionHandler = c.Name.EndsWith("Handler");
                     isExtensionHandler &= c.FullName.Contains("OrdersUseCases");
                     return isExtensionHandler;
                 })
                 .AsPublicImplementedInterfaces();
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return serviceCollection;
        }


        public static IServiceCollection AddExtensionsApiApplication(this IServiceCollection serviceCollection)
        {
            MediatRServiceConfiguration serviceConfig = new MediatRServiceConfiguration();
            ServiceRegistrar.AddRequiredServices(serviceCollection, serviceConfig);
            serviceCollection.RegisterAssemblyPublicNonGenericClasses(Assembly.GetExecutingAssembly())
                 .Where(c =>
                 {
                     bool isExtensionHandler = c.Name.EndsWith("Handler");
                     isExtensionHandler &= c.FullName.Contains("ExtensionsUseCases");
                     return isExtensionHandler;
                 })
                 .AsPublicImplementedInterfaces();
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return serviceCollection;
        }

        public static IServiceCollection AddJwtSecurity(this IServiceCollection serviceCollection, string jwtKey)
        {
            byte[] keyAsBytes = Encoding.UTF8.GetBytes(jwtKey);
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyAsBytes),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return serviceCollection;
        }
    }
}
