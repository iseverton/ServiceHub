using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("provider");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar");
        
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(500);

        builder.OwnsOne(p => p.Address, a =>
        {
            a.Property(ad => ad.Street)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .HasColumnName("street");
            
            a.Property(ad => ad.City)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .HasColumnName("city");

            a.Property(ad => ad.State)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(100)
            .HasColumnName("state");
            
            a.Property(ad => ad.ZipCode)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .HasColumnName("zip_code");
            
            a.Property(ad => ad.Country)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .HasColumnName("country");

            a.Property(ad => ad.Number)
                .IsRequired()
                .HasColumnType("int")
                .HasMaxLength(20)
                .HasColumnName("number");

            a.Property(ad => ad.Neighborhood)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .HasColumnName("neighborhood");

            a.Property(ad => ad.Complement)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .HasColumnName("complement");
        });


        builder.HasMany(p => p.Services)
            .WithOne(s => s.Provider)
            .HasForeignKey(s => s.ProviderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Service_Provider");
    }
}
