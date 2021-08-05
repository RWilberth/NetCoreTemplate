using CoreApp.Application.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Common.Exceptions
{
    public class ApiApplicationException : Exception
    {
        public ErrorDTO Error { get; private set; }

        public ApiApplicationException(ErrorDTO error)
        {
            Error = error;
        }
    }
}
