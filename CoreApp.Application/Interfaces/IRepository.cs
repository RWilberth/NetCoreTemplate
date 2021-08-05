using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreApp.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(IEnumerable<Expression<Func<T, object>>> includes);
        T Single(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        bool Create(T entity);
        bool Update(T entity); 
        Task<bool> CreateAsync(T entity);

        bool DeleteLogic(T entity, string customDeleteLogicProperty = null);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);

    }
}
