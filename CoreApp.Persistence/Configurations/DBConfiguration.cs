using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Persistence.Configurations
{
    public static class DBConfiguration
    {

        public static void UseSqlServer<TContext>(IServiceCollection serviceCollection, string connectionString, bool useLazyLoading) where TContext : DbContext
        {
            serviceCollection.AddDbContext<TContext>(options =>
            {
                options.UseLazyLoadingProxies(useLazyLoading);
                options.UseSqlServer(connectionString);
            });
        }

        public static void UseSqlServer2008<TContext>(IServiceCollection serviceCollection, string connectionString, bool useLazyLoading) where TContext : DbContext
        {
            serviceCollection.AddDbContext<TContext>(options =>
            {
                options.UseLazyLoadingProxies(useLazyLoading);
                options.UseSqlServer(connectionString);
                #pragma warning disable EF1001 // Internal EF Core API usage.
                options.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                #pragma warning restore EF1001 // Internal EF Core API usage.
            });
        }

        public static void UseMySql<TContext>(IServiceCollection serviceCollection, string connectionString, bool useLazyLoading) where TContext : DbContext
        {
            serviceCollection.AddDbContext<TContext>(options =>
            {
                options.UseLazyLoadingProxies(useLazyLoading);
                options.UseMySQL(connectionString);
            });
        }

        public static void UsePostgres<TContext>(IServiceCollection serviceCollection, string connectionString, bool useLazyLoading) where TContext : DbContext
        {
            serviceCollection.AddDbContext<TContext>(options =>
            {
                options.UseLazyLoadingProxies(useLazyLoading);
                options.UseNpgsql(connectionString);
            });
        }
    }
}
