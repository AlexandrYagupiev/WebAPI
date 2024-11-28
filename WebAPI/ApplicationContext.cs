using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
public class ApplicationContext : DbContext
{
    public DbSet<Person> Person { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Tom", Age = 37 },
                new Person { Id = 2, Name = "Bob", Age = 41 },
                new Person { Id = 3, Name = "Sam", Age = 24 }
        );
    }
}