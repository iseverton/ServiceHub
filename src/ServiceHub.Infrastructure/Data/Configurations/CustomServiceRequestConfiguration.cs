using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class CustomServiceRequestConfiguration : IEntityTypeConfiguration<CustomServiceRequest>
{
    public void Configure(EntityTypeBuilder<CustomServiceRequest> builder)
    {
        builder.ToTable("custom_service_requests");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(c => c.UserId)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(c => c.Title)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasColumnName("title")
            .HasMaxLength(100);
        
        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasColumnName("description")
            .HasMaxLength(300);
        
        builder.Property(c => c.Status)
            .HasColumnType("int")
            .HasColumnName("status")
            .IsRequired();

        builder.Property(c => c.Budget)
            .HasPrecision(18, 2)
            .HasColumnName("budget")
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("created_at");
        
        builder.Property(c => c.LastUpdatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("last_updated_at");
        
        builder.HasOne(c => c.User)
            .WithMany(u => u.CustomServices)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}
