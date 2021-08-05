using CoreApp.Application.Common.DTO;
using CoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreApp.Infrastructure.Common.Authentication
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public JwtUserDataDTO GetUserData() 
        {
            Claim userDataClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserData");
            if(userDataClaim != null)
            {
                return JsonSerializer.Deserialize<JwtUserDataDTO>(userDataClaim.Value);
            }
            return null;
        }
    }
}
