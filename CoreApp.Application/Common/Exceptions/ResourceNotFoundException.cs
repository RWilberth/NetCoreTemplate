using CoreApp.Application.Common.Constants;
using CoreApp.Application.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Common.Exceptions
{
    public class ResourceNotFoundException : ApiApplicationException
    {
        public ResourceNotFoundException(string message, string field = null) : base(new ErrorDTO {
            Code = ErrorCodes.NOT_FOUND,
            Status = (int)HttpStatusCode.NotFound,
            Message = ErrorMessages.NOT_FOUND,
            Details = new List<ErrorDetailDTO> { 
                new ErrorDetailDTO
                {
                    Field = field,
                    Message = message
                }
            }
        }) { }
    }
}
