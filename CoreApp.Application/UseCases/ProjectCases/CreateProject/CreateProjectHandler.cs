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

namespace CoreApp.Application.UseCases.ProjectCases.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, ProjectDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProjectDTO> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            DateTime dateNow = DateTime.UtcNow;
            IProjectRepository projectRepository = _unitOfWork.GetRepository<IProjectRepository>();
            Project project = new Project
            {
                Code = request.Code,
                CreatedAt = dateNow,
                Description = request.Description,
                HoldId = request.HoldId,
                PlannedStart = request.PlannedStart,
                PlannedEnd = request.PlannedEnd,
                UpdatedAt = dateNow
            };
            if(request.Activities != null)
            {
                project.Activities = request.Activities.Select(x => new Activity
                {
                    CreatedAt = dateNow,
                    TraceTool = x.TraceTool,
                    UpdatedAt = dateNow,
                    Description = x.Description
                }).ToList();
            }
            projectRepository.Create(project);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(project);
        }
    }
}
