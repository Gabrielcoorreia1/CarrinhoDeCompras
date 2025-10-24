using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PurchasingSystem.Application.Services.Abstractions;
using PurchasingSystem.Domain.User.Interfaces;
using PurchasingSystem.Infrastructure.Authentication;
using PurchasingSystem.Infrastructure.Data.Repositories;
using System.Text;

namespace PurchasingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);

            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IItemRepository, ItemRepository>();
            // services.AddScoped<IReceiptRepository, ReceiptRepository>();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind("JwtSettings", jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

            services
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                });

            return services;
        }
    }
}