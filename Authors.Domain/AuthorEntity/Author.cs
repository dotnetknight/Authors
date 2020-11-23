using Authors.Domain.CourseEntity;
using System;
using System.Collections.Generic;

namespace Authors.Domain.AuthorEntity
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MainCategory { get; set; }

        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
