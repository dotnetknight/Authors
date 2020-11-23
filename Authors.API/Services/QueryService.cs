using Authors.CQRS.Buses;
using Authors.CQRS.Interfaces;
using Authors.Repository;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Authors.API.Handlers.Query;
using Authors.Service;

namespace Authors.API.Services
{
    public class QueryService
    {
        public static void RegisterServices(ContainerBuilder builder, IServiceCollection services)
        {
            services.AddTransient<IQueryBusAsync, QueryBusAsync>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient(typeof(HATEOASLinksService), typeof(HATEOASLinksService));
            services.AddTransient(typeof(MediaTypeCheckService), typeof(MediaTypeCheckService));

            builder.RegisterAssemblyTypes(typeof(GambitQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(AuthorsQueryHandler).Assembly)
               .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
               .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(AuthorQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(AuthorCoursesQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(AuthorCourseQueryHandler).Assembly)
               .AsClosedTypesOf(typeof(IQueryHandlerAsync<,>))
               .EnableClassInterceptors();
        }
    }
}
