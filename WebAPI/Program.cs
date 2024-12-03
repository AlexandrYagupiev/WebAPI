using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfases;
using WebAPI.Servises.Implementations;
using WebAPI.Servises.Interfases;
using MassTransit;

namespace WebAPI
{
    public class Program
    { 
             
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });
            var app = builder.Build();
            app.UseDefaultFiles();
            app.UseStaticFiles();          
            app.Run();
        }      
    }
}
