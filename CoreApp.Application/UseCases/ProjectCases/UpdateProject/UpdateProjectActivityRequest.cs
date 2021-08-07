using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.UpdateProject
{
    public class UpdateProjectActivityRequest
    {
        public string Description { get; set; }
        public bool TraceTool { get; set; }
    }
}
