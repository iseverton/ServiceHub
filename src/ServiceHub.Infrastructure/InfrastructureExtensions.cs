using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Identity; // <- ADICIONE ESTA LINHA
using ServiceHub.Domain.Entities.Identity;
using ServiceHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuracao do banco de dados
        var connectionString = configuration.GetConnectionString("Server=EVERTON;Database=service_hub;Trusted_Connection=True;TrustServerCertificate=True");
        services.AddDbContext<ServiceHubDbContext>(options =>
        {
            options.UseSqlServer("Server=EVERTON;Database=service_hub;Trusted_Connection=True;TrustServerCertificate=True");
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

        
     

        return services;
    }
}