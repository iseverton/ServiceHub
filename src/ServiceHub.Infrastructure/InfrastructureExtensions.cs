using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Infrastructure.Data;
using ServiceHub.Infrastructure.Data.Repositories;
using ServiceHub.Infrastructure.Services.Auth;
using ServiceHub.Infrastructure.Services.Email;
using ServiceHub.Infrastructure.Services.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure;

public static class InfrastructureExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ServiceHubDatabase");
       

        services.AddDbContext<ServiceHubDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Identity

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredUniqueChars = 1;
        })
        .AddEntityFrameworkStores<ServiceHubDbContext>()
        .AddDefaultTokenProviders();

        services
            .AddRepositories()
            .AddServices();


        // JWt
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"]);

            jwt.TokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
            };
        });

        // Fluent Email
        var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        
        var smtpClient = new SmtpClient()
        {
            Port = emailSettings.Port,
            Host = emailSettings.Host,
            EnableSsl = emailSettings.EnableSsl,
            Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password)
        };

        services.AddFluentEmail(emailSettings.Username)
            .AddSmtpSender(smtpClient);

        Console.WriteLine("Registrado a injecao de infra");
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<IServiceRepository,ServiceRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<IProviderScheduleRepository, ProviderScheduleRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }


} 