using Authors.Domain.AuthorEntity;
using Authors.Domain.CourseEntity;
using Authors.Domain.Maps;
using Microsoft.EntityFrameworkCore;

namespace Authors.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new AuthorMap(modelBuilder.Entity<Author>());
            new CourseMap(modelBuilder.Entity<Course>());
        }
    }
}
