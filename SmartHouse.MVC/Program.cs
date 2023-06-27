using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Abstractions.Services;
using SmartHouse.Business;
using SmartHouse.Data;
using SmartHouse.Data.PassworStorage;
using SmartHouse.Repositories;
using SmartHouse.Repositories.Implementation;
using System.Configuration;

namespace SmartHouse.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
            builder.Host.UseSerilog();
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
                loggingBuilder.AddSerilog(dispose: true);
            });



            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddDbContext<HouseContext>(
                option =>
                {
                    var connString = builder.Configuration
                        .GetConnectionString("DefaultConnection");
                    option.UseSqlServer(connString);
                });
            builder.Services.AddDbContext<PassworStorageDbContext>(
                option =>
                {
                    var connString = builder.Configuration
                        .GetConnectionString("AdminDbConnection");
                    option.UseSqlServer(connString);
                });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPassworStorageRepository, PassworStorageRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

            builder.Services.AddTransient<IUserService, UserSevice>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IAuthenticatorService, AuthenticatorService>();

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/Index");
                    options.AccessDeniedPath = new PathString("/Login/Index");
                });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            app.UseSerilogRequestLogging();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}