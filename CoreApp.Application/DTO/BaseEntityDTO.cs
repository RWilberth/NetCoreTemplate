using System;

namespace CoreApp.Application.DTO
{
    public abstract class BaseEntityDTO
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
