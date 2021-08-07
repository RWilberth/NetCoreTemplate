using CoreApp.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.UpdateProject
{
    public class UpdateProjectRequest : IRequest<ProjectDTO>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string HoldId { get; set; }
        public DateTime PlannedStart { get; set; }
        public DateTime PlannedEnd { get; set; }
        public IList<UpdateProjectActivityRequest> Activities { get; set; }
    }
}
