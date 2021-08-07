using CoreApp.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.ListProjects
{
    public class ListProjectsRequest : IRequest<IEnumerable<ProjectDTO>>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
