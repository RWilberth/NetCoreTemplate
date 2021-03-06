using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public bool TraceTool { get; set; }  
        public virtual Project Project { get; set; }
    }
}
