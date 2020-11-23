using Authors.CQRS.Interfaces;
using Authors.Models.Queries;
using System.Threading.Tasks;

namespace Authors.API.Handlers.Query
{
    public class GambitQueryHandler : IQueryHandlerAsync<GambitQuery, GambitQueryResponsE>
    {
        public Task<GambitQueryResponsE> HandleAsync(GambitQuery query)
        {
            var q = query.Id;
            return Task.FromResult(new GambitQueryResponsE { Message = "THE QUEEN'S GAMBIT" });
        }
    }

    public class GambitQueryResponsE
    {
        public string Message { get; set; }
    }
}
