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
    public class ActivityMap : BaseEntityMap<Activity>
    {
        public override void Configure(EntityTypeBuilder<Activity> builder)
        {

            builder.ToTable("activities");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired();
            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired();
            builder.Property(x => x.TraceTool)
                .HasColumnName("trace_tool")
                .IsRequired();
            builder.Property(x => x.ProjectId)
                .HasColumnName("project_id")
                .IsRequired();
            builder.HasOne(x => x.Project)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.ProjectId);
            base.Configure(builder);
        }
        
    }
}
