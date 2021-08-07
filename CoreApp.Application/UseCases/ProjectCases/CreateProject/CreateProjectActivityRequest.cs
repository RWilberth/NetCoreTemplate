using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.UseCases.ProjectCases.CreateProject
{
    public class CreateProjectActivityRequest
    {
        public string Description { get; set; }
        public bool TraceTool { get; set; }
    }
}
