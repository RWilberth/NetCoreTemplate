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
    public class ProjectMap : BaseEntityMap<Project>
    {

        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();
            builder.Property(x => x.Code)
                .HasColumnName("code")
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.HoldId)
                .HasColumnName("hold_id")
                .HasMaxLength(64)
                .IsRequired();
            builder.HasMany(x => x.Activities)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);
            base.Configure(builder);
        }    
    }
}
