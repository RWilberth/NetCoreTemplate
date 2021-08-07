using AutoMapper;
using CoreApp.Application.DTO;
using CoreApp.Application.Interfaces;
using CoreApp.Application.Interfaces.Repositories;
using CoreApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.ListProjects
{
    public class ListProjectsHandler : IRequestHandler<ListProjectsRequest, IEnumerable<ProjectDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListProjectsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProjectDTO>> Handle(ListProjectsRequest request, CancellationToken cancellationToken)
        {
            IProjectRepository projectRepository = _unitOfWork.GetRepository<IProjectRepository>();
            IEnumerable<Project> projects = await projectRepository.GetFiltered(request.Code, request.Description);
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }
    }
}
