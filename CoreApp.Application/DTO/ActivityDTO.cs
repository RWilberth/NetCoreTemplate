

using CoreApp.Application.Mappings;
using CoreApp.Domain.Entities;

namespace CoreApp.Application.DTO
{
    public class ActivityDTO : BaseEntityDTO, IMapFrom<Activity>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public bool TraceTool { get; set; }  
    }
}
