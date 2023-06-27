using Microsoft.EntityFrameworkCore;
using SmartHouse.Abstractions.Data;
using SmartHouse.Abstractions.Data.Repositories;
using SmartHouse.Abstractions.Services;
using SmartHouse.Business;
using SmartHouse.Data;
using SmartHouse.Repositories;
using SmartHouse.Repositories.Implementation;
using SmartHouse.Data.PassworStorage;

namespace SmartHouse.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(typeof(Program));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HouseContext>(
                option =>
                {
                    var connString = builder.Configuration
                        .GetConnectionString("DefaultConnection");
                    option.UseSqlServer(connString);
                });
            //builder.Services.AddDbContext<PassworStorageDbContext>(
            //    option =>
            //    {
            //        var connString = builder.Configuration
            //            .GetConnectionString("AdminDbConnection");
            //        option.UseSqlServer(connString);
            //    });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPassworStorageRepository, PassworStorageRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

            builder.Services.AddTransient<IAdminService, AdminService>();

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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}