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
           
var builder = WebApplication.CreateBuilder(args);

// בדוק אם יש שגיאה בהגדרות כאן או ב-HttpsPort
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // הגדר את הפורט ל-HTTP
    // אם לא נדרש HTTPS, ניתן לבטל או לשנות את ההגדרה הזו
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // ודא שקובץ תעודת ה-SSL נכון אם משתמשים ב-HTTPS
    });
});

var app = builder.Build();

// ייתכן ש-UseHttpsRedirection יגרום לבעיות אם יש בעיה עם הגדרות HTTPS
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run(); // השגיאה עשויה להיות כאן אם יש בעיה עם ה-Kestrel




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
