using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Identity;
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServiceHub.Domain.Interfaces.Repositories;
using ServiceHub.Infrastructure.Data.Repositories;
using ServiceHub.Application.Services.Interfaces;
using ServiceHub.Infrastructure.Services.Auth;

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

        // Repositories
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();

        // Services
        services.AddScoped<IAuthService,AuthService>();


        Console.WriteLine("Registrado a injecao de infra");
        return services;
    }
}