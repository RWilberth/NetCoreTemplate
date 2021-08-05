using CoreApp.Application.Interfaces;
using CoreApp.Infrastructure.Common.Authentication;
using CoreApp.Persistence.Configurations;
using CoreApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoreApp.Infrastructure.Configurations
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraestructure<TContext>(this IServiceCollection serviceCollection, string connectionString, DataBases dataBase = DataBases.SqlServer, bool useLazyLoading = false) where TContext : DbContext
        {
            switch (dataBase)
            {
                case DataBases.SqlServer:
                    DBConfiguration.UseSqlServer<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                case DataBases.SqlServer2008:
                    DBConfiguration.UseSqlServer2008<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                case DataBases.MySql:
                    DBConfiguration.UseMySql<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                default:
                    DBConfiguration.UseSqlServer<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
            }

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<TContext>>(sp =>
            {
                TContext context = sp.GetRequiredService<TContext>();
                UnitOfWork<TContext> unitOfWork = new UnitOfWork<TContext>(context);
                RegisterRepositories<TContext>(unitOfWork);
                return unitOfWork;
            });
            Type dbContextType = typeof(TContext);
            Type baseRepositoryType = typeof(BaseRepository<,>);
            serviceCollection.RegisterAssemblyPublicNonGenericClasses(new[] {
                Assembly.GetAssembly(typeof(BaseRepository<,>))
            }).Where(repositoryType => {
                Type baseType = repositoryType.BaseType;
                if (baseType == null)
                {
                    return false;
                }
                if (!baseType.Name.Equals(baseRepositoryType.Name))
                {
                    return false;
                }
                if (baseType.GenericTypeArguments.Length < 2)
                {
                    return false;
                }
                bool hasDbContext = baseType.GenericTypeArguments.Any(x => x.Name == dbContextType.Name);
                return hasDbContext && repositoryType.Name.EndsWith("Repository");
            }).AsPublicImplementedInterfaces();
            return serviceCollection;
        }
        public static IServiceCollection AddDatabaseInfraestructure<TIUnitOfWork, TContext>(this IServiceCollection serviceCollection, string connectionString, DataBases dataBase = DataBases.SqlServer, bool useLazyLoading = false) where TContext : DbContext where TIUnitOfWork : IUnitOfWork
        {
            switch (dataBase)
            {
                case DataBases.SqlServer:
                    DBConfiguration.UseSqlServer<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                case DataBases.SqlServer2008:
                    DBConfiguration.UseSqlServer2008<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                case DataBases.MySql:
                    DBConfiguration.UseMySql<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
                default:
                    DBConfiguration.UseSqlServer<TContext>(serviceCollection, connectionString, useLazyLoading);
                    break;
            }
            serviceCollection.AddScoped(typeof(TIUnitOfWork), sp =>
            {
                TContext context = sp.GetRequiredService<TContext>();
                UnitOfWork<TContext> unitOfWork = new UnitOfWork<TContext>(context);
                RegisterRepositories<TContext>(unitOfWork);
                return unitOfWork;
            });
            Type dbContextType = typeof(TContext);
            Type baseRepositoryType = typeof(BaseRepository<,>);
            serviceCollection.RegisterAssemblyPublicNonGenericClasses(new[] {
                Assembly.GetAssembly(typeof(BaseRepository<,>))
            }).Where(repositoryType => {
                Type baseType = repositoryType.BaseType;
                if (baseType == null)
                {
                    return false;
                }
                if (!baseType.Name.Equals(baseRepositoryType.Name))
                {
                    return false;
                }
                if (baseType.GenericTypeArguments.Length < 2)
                {
                    return false;
                }
                bool hasDbContext = baseType.GenericTypeArguments.Any(x => x.Name == dbContextType.Name);
                return hasDbContext && repositoryType.Name.EndsWith("Repository");
            }).AsPublicImplementedInterfaces();
            return serviceCollection;
        }

        public static IServiceCollection AddCoreInfraestructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<IApplicationUser, ApplicationUser>();
            return serviceCollection;
        }

        private static void RegisterRepositories<TContext>(IUnitOfWork unitOfWork) where TContext : DbContext
        {
            Type baseRepositoryType = typeof(BaseRepository<,>);
            Type dbContextType = typeof(TContext);
            IEnumerable<Type> typesRepositories = System.Reflection.Assembly.GetAssembly(baseRepositoryType).GetTypes()
                .Where(persistentType =>
                {
                    bool hasRepositoryInterface = persistentType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>));
                    Type baseType = persistentType.BaseType;
                    if (baseType == null)
                    {
                        return false;
                    }
                    if (!baseType.Name.Equals(baseRepositoryType.Name))
                    {
                        return false;
                    }
                    if (baseType.GenericTypeArguments.Length < 2)
                    {
                        return false;
                    }
                    bool hasDbContext = baseType.GenericTypeArguments.Any(x => x.Name == dbContextType.Name);
                    return hasDbContext && !persistentType.IsGenericType && hasRepositoryInterface;
                });
            foreach (Type typeRepository in typesRepositories)
            {
                Type interfaceRepositoryType = typeRepository.GetInterfaces()
                    .First(interfaceOfRepository => {
                        Type[] interfaceOfInterfaceReposiutory = interfaceOfRepository.GetInterfaces();
                        bool hasIntefaceGenericRepository = interfaceOfInterfaceReposiutory.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>));
                        return !interfaceOfRepository.IsGenericType && hasIntefaceGenericRepository;
                    });
                unitOfWork.RegisterRepository(interfaceRepositoryType, typeRepository);
            }
        }
    }
}
