using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();


        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder =>
          {
            // מקבל את הפורט מהסביבה, אם לא נמצא משתמש ב-5000
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
              webBuilder.UseStartup<Startup>();
              webBuilder.UseUrls($"http://0.0.0.0:{port}");
          });
    }
}
