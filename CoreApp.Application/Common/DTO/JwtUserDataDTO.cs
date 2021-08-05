using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Application.Common.DTO
{
    public class JwtUserDataDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Uid { get; set; }
    }
}
