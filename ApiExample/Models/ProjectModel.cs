using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Models
{
    public class ProjectModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string HoldId { get; set; }
        public DateTime PlannedStart { get; set; }
        public DateTime PlannedEnd { get; set; }
        public IList<ActivityModel> Activities { get; set; }
    }
}
