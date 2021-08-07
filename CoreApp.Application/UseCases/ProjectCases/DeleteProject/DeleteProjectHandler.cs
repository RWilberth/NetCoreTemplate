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

namespace CoreApp.Application.UseCases.ProjectCases.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
        {
            IProjectRepository projectRepository = _unitOfWork.GetRepository<IProjectRepository>();
            Project project = await projectRepository.GetByIdWithActivities(request.Id);
            bool deletedSuccessfully = false;
            if(project != null)
            {
                projectRepository.Delete(project);
                await _unitOfWork.SaveChangesAsync();
                deletedSuccessfully = true;
            }
            return deletedSuccessfully;
        }
    }
}
