using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd()
            .HasColumnName("id")
            .IsRequired();

        builder.Property(s => s.Title)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .HasColumnName("title");

        builder.Property(s => s.Description)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(1000)
            .HasColumnName("description");

        builder.Property(s => s.Price)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("price");

        builder.Property(s => s.Status)
            .IsRequired()
            .HasColumnType("int")
            .HasColumnName("status");

        builder.Property(s => s.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("created_at");

        builder.Property(s => s.LastUpdatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("last_updated_at");


        builder.HasMany(s => s.Categories)
            .WithMany(sc => sc.Services)
            .UsingEntity<Dictionary<string, object>>(
                "service_serviceCategory",
                j => j
                    .HasOne<ServiceCategory>()
                    .WithMany()
                    .HasForeignKey("category_id")
                    .HasConstraintName("FK_ServiceCategory_Category")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Service>()
                    .WithMany()
                    .HasForeignKey("service_id")
                    .HasConstraintName("FK_ServiceCategory_Service")
                    .OnDelete(DeleteBehavior.Cascade)
               );
    }
}
