using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoer.Domain.Subtasks;
using ToDoer.Domain.Users;

namespace ToDoer.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);
            builder.HasMany(x=>x.ToDos).WithOne(x=>x.User).HasForeignKey(x=>x.UserId);
            builder.HasIndex(x=>x.UserName).IsUnique();
            builder.Property(user => user.UserName).IsUnicode(false).IsRequired().HasMaxLength(50);
            builder.Property(user => user.PasswordHash).IsRequired();
            builder.Property(todo => todo.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(todo => todo.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.HasQueryFilter(subtask => subtask.Status != Domain.Status.Deleted);
        }
    }
}
