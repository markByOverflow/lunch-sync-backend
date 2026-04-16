using System.Threading.RateLimiting;
using LunchSync.Api.Authentication;
using LunchSync.Api.Middleware;
using LunchSync.Api.Swagger;
using LunchSync.Core;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using LunchSync.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace LunchSync.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Dang ky service business va ha tang cho toan bo app.
        builder.Services.AddCoreServices();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy.WithOrigins("https://lunchsync.space") // Domain FE của bạn
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        // Cau hinh auth theo JWT cho user va guest.
        builder.Services.AddLunchSyncAuthentication(builder.Configuration);
        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter("auth-public", limiterOptions =>
            {
                limiterOptions.PermitLimit = 5;
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.QueueLimit = 0;
                limiterOptions.AutoReplenishment = true;
            });
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.CognitoUser, policy =>
            {
                policy.RequireClaim(AuthClaimTypes.ActorType, AuthActorTypes.User);
            });
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "LunchSync API",
                Version = "v1",
                Description = "API for LunchSync - Group Lunch Decision App"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "User token gui bang Authorization: Bearer <token>."
            });

            options.OperationFilter<AuthorizeOperationFilter>();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LunchSync API v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                Console.WriteLine($"[DEBUG] DB Connection: {context.Database.GetDbConnection().DataSource} | DB Name: {context.Database.GetDbConnection().Database}");
                Console.WriteLine("[STARTUP] Running migrations...");
                var pendingMigrations = context.Database.GetPendingMigrations().ToList();

                if (pendingMigrations.Contains("20260413073347_InitialCreate"))
                {
                    var conn = context.Database.GetDbConnection();
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT COUNT(1) FROM information_schema.tables WHERE table_name = 'collections' AND table_schema = 'public'";
                    var result = (long)cmd.ExecuteScalar()!;
                    conn.Close();

                    if (result > 0)
                    {
                        Console.WriteLine("[STARTUP] Schema already exists, stamping InitialCreate...");
                        context.Database.ExecuteSqlRaw(
                            "INSERT INTO \"__EFMigrationsHistory\" (migration_id, product_version) " +
                            "VALUES ('20260413073347_InitialCreate', '9.0.14') " +
                            "ON CONFLICT DO NOTHING");
                    }
                }

                context.Database.Migrate();

                var sqlFiles = new[] {
                    "seed_dishes.sql",
                    "seed_core_data_database_design.sql",
                    "seed_restaurant_dishes.sql"
                };

                foreach (var fileName in sqlFiles)
                {
                    var path = Path.Combine(AppContext.BaseDirectory, "Seed", fileName);
                    if (File.Exists(path))
                    {
                        Console.WriteLine($"[STARTUP] Seeding data from {fileName}...");
                        string sql = File.ReadAllText(path);

                        var connectionString = context.Database.GetConnectionString();
                        using var conn = new Npgsql.NpgsqlConnection(connectionString);
                        conn.Open();

                        try
                        {
                            using var cmd = conn.CreateCommand();
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            Console.WriteLine($"[STARTUP] Seeded {fileName} successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Error in {fileName}: {ex.Message}");
                            throw;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[STARTUP] File not found, skipping: {fileName}");
                    }
                }

                Console.WriteLine("[STARTUP] All migrations and seeding finished!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical Startup Failure: " + ex.Message);
                throw;
            }
        }


        // Gom loi domain/unhandled ve mot format response thong nhat.
        app.UseGlobalExceptionHandler();

        app.UseRateLimiter();

        app.UseCors("AllowFrontend");
        // Xac thuc truoc, phan quyen sau.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
