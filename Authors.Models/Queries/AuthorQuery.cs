using Authors.CQRS.Interfaces;
using System;

namespace Authors.Models.Queries
{
    public class AuthorQuery : IQuery
    {
        public Guid AuthorId { get; set; }
    }
}
