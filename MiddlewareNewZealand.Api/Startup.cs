using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MiddlewareNewZealand.Api.Constants;
using MiddlewareNewZealand.Api.Middlewares;
using MiddlewareNewZealand.Api.Repositories;
using MiddlewareNewZealand.Api.Repositories.Interfaces;
using MiddlewareNewZealand.Api.Services;
using MiddlewareNewZealand.Api.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Reflection;

namespace MiddlewareNewZealand.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();

            services.AddHttpClient<IMiddlewareNewZealandClient, MiddlewareNewZealandClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>(MiddlewareNewZealandConstants.BaseUrl));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareNewZealand.Api", Version = "v1" });
                c.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareNewZealand.Api v1"));
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
