using System.Threading.Tasks;

namespace Authors.CQRS.Interfaces
{
    public interface IQueryBusAsync
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
