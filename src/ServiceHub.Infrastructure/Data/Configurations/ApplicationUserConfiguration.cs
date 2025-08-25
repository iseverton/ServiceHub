using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("application_user");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasColumnName("first_name")
            .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasColumnName("last_name")
                .HasMaxLength(100);

            builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("created_at");


            builder.Property(u => u.LastUpdatedAt)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("last_updated_at");


            builder.OwnsOne(u => u.Address, a =>
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
                    .HasColumnType("nvarchar")
                    .HasMaxLength(20)
                    .HasColumnName("number");

                a.Property(ad => ad.Neighborhood)
                    .IsRequired()
                    .HasColumnType("nvarchar")
                    .HasMaxLength(100)
                    .HasColumnName("neighborhood");

                a.Property(ad => ad.Complement)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .HasColumnName("complement");

            });
        }
    }
}
