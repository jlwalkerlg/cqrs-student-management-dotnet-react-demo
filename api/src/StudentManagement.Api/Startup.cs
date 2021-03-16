using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagement.Api.Services;
using StudentManagement.Application;
using Microsoft.OpenApi.Models;
using StudentManagement.Application.Students;
using StudentManagement.Application.Courses;
using StudentManagement.Infrastructure.Data.EF;
using StudentManagement.Infrastructure.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using StudentManagement.Application.Middleware;
using StudentManagement.Api.Registration;

namespace StudentManagement.Api
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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagement API", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(Configuration["DbConnectionString"]));

            services.AddMediatR(typeof(GetCoursesQuery).GetTypeInfo().Assembly);

            services.AddScoped<ICourseDtoRepository, CourseDtoRepository>();
            services.AddScoped<IStudentDtoRepository, StudentDtoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingMiddleware<,>));
            services.AddValidators();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManagement API V1");
            });

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
