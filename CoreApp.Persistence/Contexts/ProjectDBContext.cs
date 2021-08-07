using CoreApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Contexts
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
                t =>
                {
                    if (t.BaseType == null)
                    {
                        return false;
                    }
                    if (t.BaseType.GenericTypeArguments == null || t.BaseType.GenericTypeArguments.Length == 0)
                    {
                        return false;
                    }
                    bool hasBaseEntityAsParameter = typeof(BaseEntity).IsAssignableFrom(t.BaseType.GenericTypeArguments[0]);
                    return t.IsClass && !t.IsAbstract && hasBaseEntityAsParameter;
                });
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
