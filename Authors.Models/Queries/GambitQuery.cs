using Authors.CQRS.Interfaces;

namespace Authors.Models.Queries
{
    public class GambitQuery : IQuery
    {
        public int Id { get; set; }
    }
}
