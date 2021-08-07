using CoreApp.Application.Mappings;
using CoreApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CoreApp.Application.DTO
{
    public class ProjectDTO : BaseEntityDTO, IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string HoldId { get; set; }
        public DateTime PlannedStart { get; set; }
        public DateTime PlannedEnd { get; set; }
        public virtual IList<ActivityDTO> Activities { get; set; }
    }
}
