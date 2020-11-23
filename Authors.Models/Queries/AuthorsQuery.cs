using Authors.CQRS.Interfaces;

namespace Authors.Models.Queries
{
    public class AuthorsQuery : IQuery
    {
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
    }
}
