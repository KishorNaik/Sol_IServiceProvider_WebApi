using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/32459670/resolving-instances-with-asp-net-core-di-from-within-configureservices

namespace Sol_Demo
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
            services.AddControllers();

            services.AddMemoryCache();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sol_Demo", Version = "v1" });
            });

            //ResolveDependenciesConfigureServiceMethod(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sol_Demo v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            ResolveDepedenciesConfigureMethod(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ResolveDepedenciesConfigureMethod(IApplicationBuilder applicationBuilder)
        {
            var memoryCacheService = applicationBuilder.ApplicationServices.GetRequiredService<IMemoryCache>();

            // Here you can do one time activity but you can resolve in controller side
            memoryCacheService.Set<string>("key1", "Hello");
        }

        private void ResolveDependenciesConfigureServiceMethod(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var memoryCacheService = serviceProvider.GetRequiredService<IMemoryCache>();

            // Here You can do one time activity and cannot resolve in controller side.
        }
    }
}