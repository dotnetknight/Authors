using Authors.Domain.AuthorEntity;
using System;

namespace Authors.Domain.CourseEntity
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Author Author { get; set; }

        public Guid AuthorId { get; set; }
    }
}
