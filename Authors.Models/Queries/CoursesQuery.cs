using Authors.CQRS.Interfaces;
using System;

namespace Authors.Models.Queries
{
    public class CoursesQuery : IQuery
    {
        public Guid AuthorId { get; set; }
    }
}
