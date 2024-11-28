using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
      //      string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            // получаем строку подключени€ из файла конфигурации
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // добавл€ем контекст ApplicationContext в качестве сервиса в приложение
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/api/user/seleclist", () => async (ApplicationContext db) => await db.Person.ToListAsync());

            app.MapGet("/api/user/select/{id:int}", async (int id, ApplicationContext db) =>
            {
                // получаем пользовател€ по id
                Person? person = await db.Person.FirstOrDefaultAsync(u => u.Id == id);

                // если не найден, отправл€ем статусный код и сообщение об ошибке
                if (person == null) return Results.NotFound(new { message = "ѕользователь не найден" });

                // если пользователь найден, отправл€ем его
                return Results.Json(person);
            });

            app.MapDelete("/api/user/delete/{id:int}", async (int id, ApplicationContext db) =>
            {
                // получаем пользовател€ по id
                Person? person = await db.Person.FirstOrDefaultAsync(u => u.Id == id);

                // если не найден, отправл€ем статусный код и сообщение об ошибке
                if (person == null) return Results.NotFound(new { message = "ѕользователь не найден" });

                // если пользователь найден, удал€ем его
                db.Person.Remove(person);
                await db.SaveChangesAsync();
                return Results.Json(person);
            });

            app.MapPost("/api/user/add", async (Person person, ApplicationContext db) =>
            {
                // добавл€ем пользовател€ в массив
                await db.Person.AddAsync(person);
                await db.SaveChangesAsync();
                return person;
            });

            app.MapPut("/api/user/update", async (Person personData, ApplicationContext db) =>
            {
                // получаем пользовател€ по id
                var person = await db.Person.FirstOrDefaultAsync(u => u.Id == personData.Id);

                // если не найден, отправл€ем статусный код и сообщение об ошибке
                if (person == null) return Results.NotFound(new { message = "ѕользователь не найден" });

                // если пользователь найден, измен€ем его данные и отправл€ем обратно клиенту
                person.Age = personData.Age;
                person.Name = personData.Name;
                await db.SaveChangesAsync();
                return Results.Json(person);
            });

            app.Run();
        }
    }
}
