using CoreApp.Application.Interfaces.Repositories;
using CoreApp.Domain.Entities;
using CoreApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository<Project, ProjectDBContext>, IProjectRepository
    {
        public ProjectRepository(ProjectDBContext context) : base(context)
        {

        }

        public async Task<Project> GetByIdWithActivities(int id)
        {
            return await (from project in _context.Projects.Include(x=>x.Activities)
                          where project.Id == id
                          select project).FirstOrDefaultAsync();
        }

        public async  Task<IEnumerable<Project>> GetFiltered(string code = null, string description = null)
        {
            IQueryable<Project> query = _context.Projects;
            if(!String.IsNullOrWhiteSpace(code))
            {
                query = query.Where(x => x.Code == code);
            }
            if (!String.IsNullOrWhiteSpace(description))
            {
                query = query.Where(x => EF.Functions.Like(x.Description, string.Format("%{0}%", description)));
            }
            return await query.ToListAsync();
        }
    }
}
