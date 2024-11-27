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
                // получаем пользователя по id
                Person? user = users.FirstOrDefault(u => u.Id == id);
                // если не найден, отправляем статусный код и сообщение об ошибке
                if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

                // если пользователь найден, отправляем его
                return Results.Json(user);
            });

            app.MapDelete("/api/user/delete/{id}", (string id) =>
            {
                // получаем пользователя по id
                Person? user = users.FirstOrDefault(u => u.Id == id);

                // если не найден, отправляем статусный код и сообщение об ошибке
                if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });

                // если пользователь найден, удаляем его
                users.Remove(user);
                return Results.Json(user);
            });

            app.MapPost("/api/user/add", (Person user) => {
                // устанавливаем id для нового пользователя
                user.Id = Guid.NewGuid().ToString();
                // добавляем пользователя в список
                users.Add(user);
                return user;
            });

            app.MapPut("/api/user/update", (Person userData) => {

                // получаем пользователя по id
                var user = users.FirstOrDefault(u => u.Id == userData.Id);
                // если не найден, отправляем статусный код и сообщение об ошибке
                if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
                // если пользователь найден, изменяем его данные и отправляем обратно клиенту
                user.Age = userData.Age;
                user.Name = userData.Name;
                return Results.Json(user);
            });

            app.Run();
        }
    }
}
