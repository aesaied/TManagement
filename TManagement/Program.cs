using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TManagement.Entities;

namespace TManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {

           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {

                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
               
              
            
            });

            string connStr = builder.Configuration.GetConnectionString("MyAppConnStr");

          
            //builder.Services.AddDbContextFactory<DbContext>(options => options.UseSqlServer(connStr));
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connStr)
            );

          //  builder.Services.AddDbContext<AppDbContext2>(options =>
          //options.UseSqlServer(connStr2)
          //);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //load user info
            app.UseAuthentication();
            app.UseRouting();

            //app.Use((context, next) => {

            //    if (context.User.Identity.IsAuthenticated == false) { 
                
            //    }

            //    return next();
            //});
            ////
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
