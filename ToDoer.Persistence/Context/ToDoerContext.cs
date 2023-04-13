using Microsoft.EntityFrameworkCore;
using ToDoer.Domain.Subtasks;
using ToDoer.Domain.ToDos;
using ToDoer.Domain.Users;

namespace ToDoer.Persistence.Context
{
    public class ToDoerContext : DbContext
    {


        public ToDoerContext(DbContextOptions<ToDoerContext> options) : base(options)
        {

        }


       // DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<ToDo> ToDos { get; set; }


        // Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoerContext).Assembly);
        }


    }
}
