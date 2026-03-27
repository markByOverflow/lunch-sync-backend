using LunchSync.Api.Authentication;
using LunchSync.Api.Middleware;
using LunchSync.Api.Services;
using LunchSync.Core;
using LunchSync.Core.Common.Auth;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Infrastructure;
using Microsoft.OpenApi.Models;

namespace LunchSync.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Dang ky service business va ha tang cho toan bo app.
        builder.Services.AddCore();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        // Cau hinh auth theo JWT cho host/user va guest.
        builder.Services.AddLunchSyncAuthentication(builder.Configuration);

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.CognitoUser, policy =>
            {
                policy.RequireClaim(AuthClaimTypes.ActorType, AuthActorTypes.User);
            });

            options.AddPolicy(AuthPolicies.Guest, policy =>
            {
                policy.RequireClaim(AuthClaimTypes.ActorType, AuthActorTypes.Guest);
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
                Description = "Host/User token gui bang Authorization: Bearer <token>."
            });

            options.AddSecurityDefinition("GuestToken", new OpenApiSecurityScheme
            {
                Name = AuthHeaderNames.GuestToken,
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Guest JWT issued by POST /api/auth/guest-token."
            });
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
        }

        app.UseHttpsRedirection();

        // Gom loi domain/unhandled ve mot format response thong nhat.
        app.UseGlobalExceptionHandler();

        // Xac thuc truoc, phan quyen sau.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
