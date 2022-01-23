using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;

namespace WebApi
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

            // Dependency Injection modülüne DbContext'in verilmesi 
            services.AddDbContext<CounselingCenterDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "CounselingCenterDB"));


            // Swagger Implementation
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo {Title = "CounselingCenter", Version = "v1"});
            });
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "CounselingCenter v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization(); 

            app.UseCustomExceptionMiddleware(); // Custom Exception Middleware

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}