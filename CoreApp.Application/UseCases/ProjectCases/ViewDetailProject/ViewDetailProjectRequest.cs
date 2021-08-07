using CoreApp.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.ViewDetailProject
{
    public class ViewDetailProjectRequest : IRequest<ProjectDTO>
    {
        public int Id { get; set; }
    }
}
