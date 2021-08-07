using ApiExample.Models;
using CoreApp.Application.DTO;
using CoreApp.Application.UseCases.ProjectCases.CreateProject;
using CoreApp.Application.UseCases.ProjectCases.DeleteProject;
using CoreApp.Application.UseCases.ProjectCases.ListProjects;
using CoreApp.Application.UseCases.ProjectCases.UpdateProject;
using CoreApp.Application.UseCases.ProjectCases.ViewDetailProject;
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
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<ProjectDTO>> GetFiltered(string code = null, string description = null)
        {
            return await _mediator.Send(new ListProjectsRequest { 
                Code = code,
                Description = description
            });
        }

        [HttpGet("{id}")]
        public async Task<ProjectDTO> Get(int id)
        {
            return await _mediator.Send(new ViewDetailProjectRequest
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ProjectDTO> Create([FromBody] CreateProjectRequest model)
        {
            return await _mediator.Send(model);
        }

        [HttpPut("{id}")]
        public async Task<ProjectDTO> Update(int id, [FromBody] ProjectModel model)
        {
            var request = new UpdateProjectRequest
            {
                Id = id,
                Code = model.Code,
                Description = model.Description,
                PlannedEnd = model.PlannedEnd,
                HoldId = model.HoldId,
                PlannedStart = model.PlannedStart
            };
            if(model.Activities != null)
            {
                request.Activities = model.Activities.Select(x => new UpdateProjectActivityRequest
                {
                    Description = x.Description,
                    TraceTool = x.TraceTool
                }).ToList();
            }
            return await _mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _mediator.Send(new DeleteProjectRequest
            {
                Id = id
            });
        }
    }
}
