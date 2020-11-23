using Authors.CQRS.Interfaces;
using Autofac;
using System;
using System.Threading.Tasks;

namespace Authors.CQRS.Buses
{
    public class QueryBusAsync : IQueryBusAsync
    {
        private readonly ILifetimeScope context;

        public QueryBusAsync(ILifetimeScope context) => this.context = context ?? throw new ArgumentNullException(nameof(context));

        public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            if (query == null)
                throw new ArgumentNullException($"Query shouldn't be null");

            using (var scope = context.BeginLifetimeScope())
            {
                var handler = scope.Resolve<IQueryHandlerAsync<TQuery, TResult>>()
                    ?? throw new InvalidOperationException($"Handler not found for specified query");

                return handler.HandleAsync(query);
            }
        }
    }
}
