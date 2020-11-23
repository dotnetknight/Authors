using Authors.CQRS.Interfaces;
using System;

namespace Authors.Models.Queries
{
    public class AuthorCoursesQuery : IQuery
    {
        public Guid AuthorId { get; set; }
    }
}
