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

namespace CoreApp.Application.UseCases.ProjectCases.ListActivities
{
    public class ListActivitiesHandler : IRequestHandler<ListActivitiesRequest, IEnumerable<ActivityDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListActivitiesHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ActivityDTO>> Handle(ListActivitiesRequest request, CancellationToken cancellationToken)
        {
            IActivityRepository activityRepository = _unitOfWork.GetRepository<IActivityRepository>();
            IEnumerable<Activity> activities = await activityRepository.FilterAsync(x=>x.ProjectId == request.ProjectId);
            return _mapper.Map<IEnumerable<ActivityDTO>>(activities);
        }
    }
}
