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
        builder.ToTable("providers");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(p => p.Name)
              .HasColumnType("nvarchar(100)")
              .HasColumnName("name")
              .IsRequired();

        builder.Property(p => p.Description)
                .HasColumnType("nvarchar(200)")
                .HasColumnName("description")
                .IsRequired();

        builder.HasOne(p => p.ApplicationUser)
           .WithOne()
           .HasForeignKey<Provider>(p => p.ApplicationUserId)
           .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(p => p.Services)
            .WithOne(s => s.Provider)
            .HasForeignKey(s => s.ProviderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Service_Provider");
    }
}
