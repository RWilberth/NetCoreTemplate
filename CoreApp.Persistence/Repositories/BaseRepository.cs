using CoreApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Repositories
{
    public abstract class BaseRepository<T, TContext> : IRepository<T> where T : class where TContext : DbContext
    {
        protected readonly TContext _context;

        private const string DELETE_LOGIC_PROPERTY = "Status";

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(IEnumerable<Expression<Func<T, object>>> includes)
        {
            List<string> includelist = new List<string>();
            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("El cuerpo debe ser una expresion");

                includelist.Add(body.Member.Name);
            }
            IQueryable<T> query = _context.Set<T>();
            includelist.ForEach(x => query = query.Include(x));
            return query.ToList();
        }


        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("El cuerpo debe ser una expresion");

                includelist.Add(body.Member.Name);
            }

            IQueryable<T> query = _context.Set<T>();

            includelist.ForEach(x => query = query.Include(x));

            return query.Where(predicate).ToList();
            
        }
        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, List<string> includes)
        {
            IQueryable<T> query = _context.Set<T>();
            includes.ForEach(x => query = query.Include(x));
            return query.Where(predicate).ToList();
            
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Where(predicate).Count();
        }

        public bool Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar", ex);
            }
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar", ex);
            }
        }

        public bool Update(T entity)
        {
                try
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar", ex);
                }
        }

        public void Delete(T entity)
        {
                _context.Entry(entity).State = EntityState.Deleted;
            
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entities = _context.Set<T>().Where(predicate).ToList();
            entities.ForEach(x => _context.Entry(x).State = EntityState.Deleted);
        }

        private string GetByKeyProperty(Type tipo)
        {
            string nameSpacePrincipal = tipo.Namespace;
            IList<PropertyInfo> properties = new List<PropertyInfo>(tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance));

            foreach (PropertyInfo propiedadLlave in properties)
            {
                if (!propiedadLlave.CanRead)
                {
                    continue;
                }
                System.ComponentModel.DataAnnotations.KeyAttribute attribute =
                    propiedadLlave.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>();

                if (attribute == null)
                {
                    continue;
                }
                return propiedadLlave.Name;
            }
            return null;
        }
        public IEnumerable<T> GetByIds(IList<int> ids)
        {
            Type tipo = typeof(T);
            Expression listIds = Expression.Constant(ids);
            ParameterExpression parameter = Expression.Parameter(tipo, "x");
            string nombrePropiedadLlave = this.GetByKeyProperty(tipo);
            Expression property = Expression.Property(parameter, nombrePropiedadLlave);
            MethodInfo method = typeof(List<int>).GetMethod("Contains", new[] { typeof(int) });
            Expression containsExpression = Expression.Call(listIds, method, property);
            Expression<Func<T, bool>> lambda =
               Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
            return this.Filter(lambda);
        }

        private Expression GetContainsExpresion(string propertyName, string propertyValue, Type tipo)
        {
            var parameterExp = Expression.Parameter(tipo, "x");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public bool DeleteLogic(T entity, string customDeleteLogicProperty = null) {
            string currentDeleteLogicProperty = DELETE_LOGIC_PROPERTY;
            if (!String.IsNullOrWhiteSpace(customDeleteLogicProperty))
            {
                currentDeleteLogicProperty = customDeleteLogicProperty;
            }
            Type entityType = typeof(T);
            PropertyInfo deleteLogicPropertyInfo = entityType.GetProperty(currentDeleteLogicProperty);
            if(deleteLogicPropertyInfo == null) 
            {
                throw new Exception(String.Format("The entity must have a control property to delete logic {0}", currentDeleteLogicProperty));
            }
            if(deleteLogicPropertyInfo.PropertyType != typeof(bool))
            {
                throw new Exception(String.Format("The property control {0} must be booelan", currentDeleteLogicProperty));
            }
            deleteLogicPropertyInfo.SetValue(entity, false);
            return this.Update(entity);
        }

    }
}
