using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfases;
using WebAPI.Servises.Implementations;
using WebAPI.Servises.Interfases;

namespace WebAPI
{
    public class Program
    { 
             
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();          
            var app = builder.Build();
            app.UseDefaultFiles();
            app.UseStaticFiles();          
            app.Run();
        }

       
        

      
    }
}
