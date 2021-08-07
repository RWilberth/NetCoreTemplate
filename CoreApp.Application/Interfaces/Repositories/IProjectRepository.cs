using CoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        public Task<IEnumerable<Project>> GetFiltered(string code = null, string description = null);
        public Task<Project> GetByIdWithActivities(int id);
    }
}
