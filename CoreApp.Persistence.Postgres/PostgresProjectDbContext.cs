using CoreApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Postgres
{
    public class PostgresProjectDbContext : ProjectDBContext
    {
        public PostgresProjectDbContext() : base(new DbContextOptionsBuilder<ProjectDBContext>()
                 .UseNpgsql("Host=localhost;Database=ProjectsDB;Username=postgres;Password=admin")
                 .Options)
        {

        }
    }
}
