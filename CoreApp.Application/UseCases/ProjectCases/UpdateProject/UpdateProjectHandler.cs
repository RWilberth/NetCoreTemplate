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

namespace CoreApp.Application.UseCases.ProjectCases.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, ProjectDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProjectDTO> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            DateTime dateNow = DateTime.UtcNow;
            IProjectRepository projectRepository = _unitOfWork.GetRepository<IProjectRepository>();
            IActivityRepository activityRepository = _unitOfWork.GetRepository<IActivityRepository>();
            Project project = await projectRepository.GetByIdWithActivities(request.Id);
            project.Code = request.Code;
            project.Description = request.Description;
            project.HoldId = request.HoldId;
            project.PlannedEnd = request.PlannedEnd;
            project.PlannedStart = request.PlannedStart;
            project.UpdatedAt = dateNow;
            foreach (Activity activity in project.Activities)
            {
                activityRepository.Delete(activity);
            }
            if (request.Activities != null)
            {
                project.Activities = request.Activities.Select(x => new Activity
                {
                    CreatedAt = dateNow,
                    TraceTool = x.TraceTool,
                    UpdatedAt = dateNow,
                    Description = x.Description,
                    ProjectId = project.Id
                }).ToList();
                /*
                foreach(Activity activityToCreate in project.Activities)
                {
                    activityRepository.Create(activityToCreate);
                }*/
            }
            projectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(project);
        }
    }
}
