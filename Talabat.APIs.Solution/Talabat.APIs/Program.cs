using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        

        public static async  Task   Main(string[] args)
        {

           

            //Part 1
            var webApplicationBuilder = WebApplication.CreateBuilder(args);
            //Part 2
            #region Configure Services
            // Add services to the container.
            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddSwaggerServices();
            //user configure services
            webApplicationBuilder.Services.AddDbContext<StoreContext>(options=>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });


            webApplicationBuilder.Services.AddDbContext<AppIdentityDbContext>(options=>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });


        

            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>(serviceProdvider=>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

      

            webApplicationBuilder.Services.AddApplicationServices();
            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            #endregion

            //Part 3
            var app = webApplicationBuilder.Build();

            //Part 4
            #region Apply Migrations Migrations & Data Seeding  

            



            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();     // Update Database for business
                await _identityDbContext.Database.MigrateAsync();     // Update Database for Security
                await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
                var _userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(_userManager); // Data Seeding for user 
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error To Add Migations");
            }
            #endregion


            //Part 5
            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          
         

            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion
            //Part 6
            app.Run();
        }
    }
}
