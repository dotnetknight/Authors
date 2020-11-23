using System.Threading.Tasks;

namespace Authors.CQRS.Interfaces
{
    public interface IQueryHandlerAsync<TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
