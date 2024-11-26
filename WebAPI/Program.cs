using WebAPI.Models;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Person> users = new List<Person>
            {
                  new() { Id = "1", Name = "Tom", Age = 37 },
                  new() { Id = "2", Name = "Ember", Age = 23 },
                  new() { Id = "3", Name = "Dima", Age = 33 }
            };

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/api/user/seleclist", () => users);

            app.MapGet("/api/user/select/{id}", (string id) =>
            {
                // �������� ������������ �� id
                Person? user = users.FirstOrDefault(u => u.Id == id);
                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ���������� ���
                return Results.Json(user);
            });

            app.MapDelete("/api/user/delete/{id}", (string id) =>
            {
                // �������� ������������ �� id
                Person? user = users.FirstOrDefault(u => u.Id == id);

                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ������� ���
                users.Remove(user);
                return Results.Json(user);
            });

            app.MapPost("/api/user/add", (Person user) => {

                // ��������� ������������ � ������
                users.Add(user);
                return user;
            });

            app.MapPut("/api/user/update", (Person userData) => {

                // �������� ������������ �� id
                var user = users.FirstOrDefault(u => u.Id == userData.Id);
                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
                // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������
                user.Age = userData.Age;
                user.Name = userData.Name;
                return Results.Json(user);
            });

            app.Run();
        }
    }
}
