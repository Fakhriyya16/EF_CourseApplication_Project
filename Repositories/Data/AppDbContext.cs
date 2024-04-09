using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Group = Domain.Models.Group;


namespace Repositories.Data
{
    public class AppDbContext<T> : DbContext 
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=DESKTOP-RB3F59F\\MSSQLSERVER01;initial catalog=EF_CourseApplication_Project;trusted_connection=true");
        }
    }
}
