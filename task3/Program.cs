using BusinessLogic.Services;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Domain.Models;
using Domain.Interfaces;

namespace task3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Система управления проектной деятельностью",
                    Description = "Система предназначена для организации проектной деятельности в образовательном учреждении (колледже/вузе). \n" +
                    "\nПозволяет управлять учебными курсами, студенческими группами, проектами и оценками.",
                    Contact = new OpenApiContact
                    {
                        Name = "Пример контакта",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Пример лицензии",
                        Url = new Uri("https://example.com/license")
                    }
                });


                options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

                try
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        options.IncludeXmlComments(xmlPath);
                    }
                }
                catch
                {
                }
            });

            builder.Services.AddDbContext<Task2DbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=1357;Database=task2_DB;Username=postgres;Password=root111;"));

            builder.Services.AddScoped<Domain.Interfaces.IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IGradeService, GradeService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectTeamService, ProjectTeamService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IStudentTeamService, StudentTeamService>();
            //builder.Services.AddScoped<ISubmissionService, SubmissionService>();
            //builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
                    options.RoutePrefix = "swagger";
                });
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}