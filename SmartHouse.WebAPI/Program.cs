using Microsoft.AspNetCore.Authentication.Cookies;
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

namespace SmartHouse.WebAPI
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}