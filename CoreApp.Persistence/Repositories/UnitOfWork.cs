using CoreApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private TContext Context { get; }

        private IDictionary<Type, object> _repositories { get; set; }
        public UnitOfWork(TContext context)
        {
            Context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
        public T GetRepository<T>() where T : class
        {
            return (T)_repositories[typeof(T)];
        }
        public void RegisterRepository<TRepositoryInterface, TRepository>()
        {
            this.RegisterRepository(typeof(TRepositoryInterface), typeof(TRepository));
        }
        public void RegisterRepository(Type interfaceRepositoryType, Type repositoryType)
        {
            object instance = Activator.CreateInstance(repositoryType, Context);
            _repositories.Add(interfaceRepositoryType, instance);
        }
    }
}
