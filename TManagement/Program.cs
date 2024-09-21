using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using TManagement.AppServices.Account;
using TManagement.AppServices.Loockups;
using TManagement.Entities;

namespace TManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {

           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options => {
            
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());   
            }
                
                ).AddViewLocalization();

            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddScoped<ILoockupAppService, LoockupAppService>();
            builder.Services.AddScoped<IAccountAppService, AccountAppService>();    

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



            //string.Format("Welecome {0}", "Alaa");

            //  Register localization

            builder.Services.Configure<MvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                  factory.Create(typeof(TManagementApp));
            });
          
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-JO")
                 };
                options.DefaultRequestCulture = new RequestCulture(culture: "ar-JO", uiCulture: "ar-JO");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                //options.RequestCultureProviders.Clear();
               // options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

            });

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
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

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
