using Authors.CQRS.Interfaces;

namespace Authors.Models.Queries
{
    public class AuthorsByMainCategoryQuery : IQuery
    {
        public string MainCategory { get; set; }
    }
}
