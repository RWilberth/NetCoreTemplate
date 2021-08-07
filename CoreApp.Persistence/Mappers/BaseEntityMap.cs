using CoreApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Persistence.Mappers
{
    public abstract class BaseEntityMap<TEntity> : IEntityTypeConfiguration<TEntity>
     where TEntity : BaseEntity
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");
        }
    }
}
