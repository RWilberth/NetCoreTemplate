using CoreApp.Application.DTO;
using CoreApp.Application.UseCases.ProjectCases.ListActivities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<ActivityDTO>> GetFiltered(int projectId)
        {
            return await _mediator.Send(new ListActivitiesRequest
            {
                ProjectId = projectId
            });
        }
    }
}
