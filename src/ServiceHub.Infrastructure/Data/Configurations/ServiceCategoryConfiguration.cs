using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("service_category");
        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(sc => sc.Name)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(sc => sc.Description)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .HasColumnName("description");


    }
}
