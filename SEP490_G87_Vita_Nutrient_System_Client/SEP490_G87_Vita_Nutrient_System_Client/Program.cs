using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace SEP490_G87_Vita_Nutrient_System_Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var googleAuthSettings = builder.Configuration.GetSection("GoogleAuth");

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Cấu hình Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Home/Login"; // Đường dẫn khi chưa đăng nhập
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session timeout
            })
            .AddGoogle(options =>
            {
                options.ClientId = googleAuthSettings["ClientId"];
                options.ClientSecret = googleAuthSettings["ClientSecret"];
                options.CallbackPath = googleAuthSettings["CallbackPath"];
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            Console.WriteLine("Environment: " + app.Environment.EnvironmentName);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
