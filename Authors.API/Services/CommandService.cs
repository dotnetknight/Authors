using Authors.API.Handlers.Command;
using Authors.CQRS.Buses;
using Authors.CQRS.Interfaces;
using Authors.Repository;
using Authors.Service;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Authors.API.Services
{
    public class CommandService
    {
        public static void RegisterServices(ContainerBuilder builder, IServiceCollection services)
        {
            services.AddTransient<ICommandBusAsync, CommandBusAsync>();
            services.AddScoped(typeof(HATEOASLinksService), typeof(HATEOASLinksService));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.RegisterAssemblyTypes(typeof(CreateAuthorCommandHandler).Assembly)
               .AsClosedTypesOf(typeof(ICommandHandlerAsync<,>))
               .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(UpdateCourseForAuthorCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<>))
                .EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(DeleteAuthorCourseCommandHandler).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandlerAsync<>))
                .EnableClassInterceptors();
        }
    }
}
