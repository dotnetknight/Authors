using System;
using System.IO;
using System.Reflection;
using Authors.API.Filters;
using Authors.API.Services;
using Authors.Repository;
using Authors.Service;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

namespace Authors.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();

            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<ApiExceptionAttribute>();
                options.Filters.Add(new ProducesAttribute("application/json", "application/xml", "application/vnd.marvin.hateoas+json"));
                options.Filters.Add(new ConsumesAttribute("application/json", "application/xml"));
            })
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddXmlDataContractSerializerFormatters();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(typeof(HATEOASLinksService), typeof(HATEOASLinksService));

            services.AddSwaggerGen(x =>
            {
                x.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var container = new ContainerBuilder();

            CommandService.RegisterServices(container, services);
            QueryService.RegisterServices(container, services);

            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authors API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
