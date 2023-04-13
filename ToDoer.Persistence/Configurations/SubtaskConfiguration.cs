using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Domain.Subtasks;

namespace ToDoer.Persistence.Configurations
{
    internal class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.ToTable("Subtasks");
            builder.HasKey(subtask => subtask.Id);
            builder.Property(subtask => subtask.Title).IsRequired().HasMaxLength(100);
            builder.Property(subtask => subtask.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(subtask  => subtask.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.HasQueryFilter(subtask => subtask.Status != Domain.Status.Deleted);
        }
    }
}
