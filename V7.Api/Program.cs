using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using V7.Api.Extensions;
using V7.Domain.Entites.Identity;
using V7.Infrastructure.Data.Context;
using V7.Infrastructure.Identity;
using V7.Api.Middleware;
using V7.Infrastructure.Data;
using StackExchange.Redis;

namespace V7.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configurations
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<V7Db>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection= builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            #endregion
            var app = builder.Build();

            #region Update DataBase
            using var scope = app.Services.CreateScope();


            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<V7Db>();
                await context.Database.MigrateAsync();
                //await V7DataSeed.SeedAsync(context);
                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUserAsync(services.GetRequiredService<UserManager<AppUser>>());

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }


            #endregion

            #region Configur HTTP Requset
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
