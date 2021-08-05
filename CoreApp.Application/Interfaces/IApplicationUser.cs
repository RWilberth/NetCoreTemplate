using CoreApp.Application.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Interfaces
{
    public interface IApplicationUser
    {
        JwtUserDataDTO GetUserData();
    }
}
