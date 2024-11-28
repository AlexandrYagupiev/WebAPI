using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/api/users", async (ApplicationContext db) => await db.Person.ToListAsync());

            app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
            {
                // �������� ������������ �� id
                Person? person = await db.Person.FirstOrDefaultAsync(u => u.Id == id);

                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (person == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ���������� ���
                return Results.Json(person);
            });

            app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
            {
                // �������� ������������ �� id
                Person? person = await db.Person.FirstOrDefaultAsync(u => u.Id == id);

                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (person == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ������� ���
                db.Person.Remove(person);
                await db.SaveChangesAsync();
                return Results.Json(person);
            });

            app.MapPost("/api/users", async (Person person, ApplicationContext db) =>
            {
                // ��������� ������������ � ������
                await db.Person.AddAsync(person);
                await db.SaveChangesAsync();
                return person;
            });

            app.MapPut("/api/users", async (Person personData, ApplicationContext db) =>
            {
                // �������� ������������ �� id
                var person = await db.Person.FirstOrDefaultAsync(u => u.Id == personData.Id);

                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (person == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
                person.Age = personData.Age;
                person.Name = personData.Name;
                await db.SaveChangesAsync();
                return Results.Json(person);
            });

            app.Run();
        }
    }
}
