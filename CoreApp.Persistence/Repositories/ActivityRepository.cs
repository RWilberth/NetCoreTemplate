using CoreApp.Application.Interfaces.Repositories;
using CoreApp.Domain.Entities;
using CoreApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Repositories
{
    public class ActivityRepository : BaseRepository<Activity, ProjectDBContext>, IActivityRepository
    {
        public ActivityRepository(ProjectDBContext context) : base(context)
        {

        }
    }
}
