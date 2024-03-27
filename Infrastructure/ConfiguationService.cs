using Application.Services;
using DemoSendMail.Services;
using Domain.Entities.Settings;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class ConfiguationService
    {
        public static IServiceCollection AddInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration) 
        {
            services.AddDbContext<OHDDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection string 'Database not found'"))
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                    };
                });

            services.Configure<AsuzeOptions>(configuration.GetSection("CloudStorage"));

            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddTransient<IRandomService, RandomService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IBCryptService, BCryptService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
