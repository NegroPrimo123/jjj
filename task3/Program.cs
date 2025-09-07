using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace task3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем сервисы в контейнер
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Настройка базы данных (PostgreSQL)
            builder.Services.AddDbContext<Task2DbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=1357;Database=task2_DB;Username=postgres;Password=root111;"));

            // Регистрация зависимостей
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IStudentService, StudentService>();

            var app = builder.Build();

            // Настройка конвейера HTTP запросов
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            
            // Маршрутизация
            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            // Запускаем бесконечное ожидание запросов
            app.Run(); // Это предотвратит завершение программы
        }
    }
}