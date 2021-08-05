using System;
using System.Threading.Tasks;

namespace CoreApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        void Dispose();

        int SaveChanges();
        Task<int> SaveChangesAsync();
        T GetRepository<T>() where T : class;
        void RegisterRepository<TRepositoryInterface, TRepository>();
        void RegisterRepository(Type interfaceRepositoryType, Type repositoryType);
    }
}
