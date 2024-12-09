using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfases;
using WebAPI.Servises.Implementations;
using WebAPI.Servises.Interfases;
using MassTransit;
using Swashbuckle.AspNetCore.Swagger;
using WebAPI.RabbitMQ;

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
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();   
            builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
            var app = builder.Build();
            app.MapControllers();
            app.UseDeveloperExceptionPage();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            }
           
            app.UseDefaultFiles();
            app.UseStaticFiles();          
            app.Run();
        }      
    }
}
