using Authors.Domain.AuthorEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authors.Domain.Maps
{
    public class AuthorMap
    {
        public AuthorMap(EntityTypeBuilder<Author> entityBuilder)
        {
            entityBuilder.HasKey(p => p.Id);

            entityBuilder.Property(p => p.FirstName)
                .IsRequired();

            entityBuilder.Property(p => p.LastName)
                .IsRequired();

            entityBuilder.Property(p => p.MainCategory)
                .HasMaxLength(50);

            entityBuilder.HasMany(p => p.Courses)
                .WithOne(t => t.Author)
                .HasForeignKey(t => t.AuthorId);
        }
    }
}
