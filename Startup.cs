using api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
   {
        // Define a policy named "AllowSpecificOrigin"
        options.AddPolicy("AllowSpecificOrigin",
           builder =>
           {
                // Allow requests from the specified origin
                builder.WithOrigins("https://location-client.onrender.com/") // URL of your Angular app
                      .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                      .AllowAnyHeader(); // Allow any header in requests
            });
   });


            services.AddTransient<ManagQuery>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy defined earlier
            // Use Swagger in all environments
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
                c.RoutePrefix = string.Empty;  // Set Swagger UI at the app's root
            });

            app.UseAuthorization();


            // Map default controller routes
            app.UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller}/{action=Index}/{id?}"); // MVC routes

           endpoints.MapFallbackToFile("index.html"); // Serve index.html for Angular
       });

        }
    }
}
