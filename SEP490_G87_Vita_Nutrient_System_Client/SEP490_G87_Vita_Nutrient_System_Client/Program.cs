using Microsoft.AspNetCore.Authentication.Cookies;

namespace SEP490_G87_Vita_Nutrient_System_Client
{
    public class Program()
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDistributedMemoryCache(); // Sử dụng bộ nhớ trong để lưu trữ session (yêu cầu)

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session timeout
                });


            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=PlanUser}/{id?}");

            app.Run();
        }
    }

}