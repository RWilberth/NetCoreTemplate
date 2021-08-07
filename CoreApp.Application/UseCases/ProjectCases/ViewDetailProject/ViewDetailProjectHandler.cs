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

namespace CoreApp.Application.UseCases.ProjectCases.ViewDetailProject
{
    public class ViewDetailProjectHandler : IRequestHandler<ViewDetailProjectRequest, ProjectDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ViewDetailProjectHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProjectDTO> Handle(ViewDetailProjectRequest request, CancellationToken cancellationToken)
        {
            IProjectRepository projectRepository = _unitOfWork.GetRepository<IProjectRepository>();
            Project project = await projectRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ProjectDTO>(project);
        }
    }
}
