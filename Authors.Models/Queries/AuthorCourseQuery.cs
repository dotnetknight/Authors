using Authors.CQRS.Interfaces;
using System;

namespace Authors.Models.Queries
{
    public class AuthorCourseQuery : IQuery
    {
        public Guid AuthorId { get; set; }
        public Guid CourseId { get; set; }
    }
}
