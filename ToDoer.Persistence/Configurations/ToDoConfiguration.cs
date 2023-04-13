using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Domain.Subtasks;
using ToDoer.Domain.ToDos;

namespace ToDoer.Persistence.Configurations
{
    internal class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
    {
        public void Configure(EntityTypeBuilder<ToDo> builder)
        {
            builder.ToTable("Todos");
            builder.HasKey(toDo => toDo.Id);
            builder.HasMany(todo => todo.Subtasks).WithOne(subtask => subtask.ToDo).HasForeignKey(subt=> subt.TodoItemId);
            builder.Property(toDo => toDo.Title).IsRequired().HasMaxLength(100);
            builder.Property(todo => todo.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
            builder.Property(todo => todo.TargetCompletionDate).HasColumnType("datetime");
            builder.Property(todo=>todo.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(todo => todo.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.HasQueryFilter(subtask => subtask.Status != Domain.ToDos.Status.Deleted);

        }
    }
}

            //builder.HasOne(todo => todo.User).WithMany(user => user.ToDos);