using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.Domain.Entities;
using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data;

public class ServiceHubDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ServiceHubDbContext(DbContextOptions<ServiceHubDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCategory> Categories { get; set; }
    public DbSet<ServiceReview> ServiceReviews { get; set; }
    public DbSet<CustomServiceRequest> CustomServiceRequests { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



}
