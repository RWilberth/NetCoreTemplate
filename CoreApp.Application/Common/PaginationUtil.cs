using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Application.Common
{
    public static class PaginationUtil
    {
        public static int GetSkip(int page, int pageSize) 
        {
            return (page - 1) * pageSize;
        }
    }
}
