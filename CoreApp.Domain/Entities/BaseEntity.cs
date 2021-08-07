using System;

namespace CoreApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
