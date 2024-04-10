using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Group = Domain.Models.Group;


namespace Repositories.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Education> Educations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var createdDateProperty = entityType.FindProperty("CreatedDate");
                if (createdDateProperty != null && createdDateProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>("CreatedDate")
                        .HasDefaultValueSql("GETDATE()");
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=DESKTOP-RB3F59F\\MSSQLSERVER01;initial catalog=EF_CourseApplication_Project;trusted_connection=true");
        }
    }
}
