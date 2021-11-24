using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Interfaces;
using PerformancetrackerApi.Repository;

namespace PerformancetrackerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(_ => new DbContext(Configuration["ConnectionStrings:SqlConnection"]));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGradesRepository, GradesRepository>();
            services.AddScoped<IDueDatesRepository, DueDatesRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                
                //c.SwaggerDoc("v1", new OpenApiInfo {Title = "Performancetracker API", Version = "V1"});
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "The .NET Lab API",
                    Description = "An imaginary API of my blog",
                    TermsOfService = new Uri("https://blog.georgekosmidis.net/privacy-policy/"),
                    Contact = new OpenApiContact
                    {
                        Name = "George Kosmidis",
                        Email = string.Empty,
                        Url = new Uri("https://georgekosmidis.net"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://blog.georgekosmidis.net/privacy-policy/"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Performancetracker API V1"));
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Performancetracker API V1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}