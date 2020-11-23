using Authors.Domain.CourseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authors.Domain.Maps
{
    public class CourseMap
    {
        public CourseMap(EntityTypeBuilder<Course> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);

            entityBuilder.Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(p => p.Description)
                .HasMaxLength(1500)
                .IsRequired();
        }
    }
}
