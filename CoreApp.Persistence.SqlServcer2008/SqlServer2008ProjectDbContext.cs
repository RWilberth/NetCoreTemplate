using CoreApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.SqlServcer2008
{
    public class SqlServer2008ProjectDbContext : ProjectDBContext
    {
        public SqlServer2008ProjectDbContext() : base(new DbContextOptionsBuilder<ProjectDBContext>()
                 .UseSqlServer("Data Source=LAPTOP-CIOJSRHB\\SQLSERVER2018;Initial Catalog=ProjectsDB;User Id=sa;Password=4dm1n")
                 .ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>()
                 .Options)
        {

        }
    }
}
