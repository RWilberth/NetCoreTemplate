using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Application.Common.DTO
{
    public class ErrorDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public IList<ErrorDetailDTO> Details { get; set; }
        public string Code { get; set; }
    }
}
