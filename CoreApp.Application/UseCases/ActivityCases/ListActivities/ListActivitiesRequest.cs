using CoreApp.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.ListActivities
{
    public class ListActivitiesRequest : IRequest<IEnumerable<ActivityDTO>>
    {
        public int ProjectId { get; set; }
    }
}
