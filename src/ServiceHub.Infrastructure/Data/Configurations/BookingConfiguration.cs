using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("booking");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(b => b.ServiceId)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("service_id")
            .IsRequired();

        builder.Property(b => b.ClientId)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("client_id")
            .IsRequired();

        builder.Property(b => b.ProviderId)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("provider_id")
            .IsRequired();

        builder.Property(b => b.ScheduledAt)
            .HasColumnType("datetime")
            .HasColumnName("scheduled_at")
            .IsRequired();

        builder.Property(b => b.Status)
            .HasColumnType("int")
            .HasColumnName("status")
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasColumnType("datetime")
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasOne(b => b.Service)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Client)
            .WithMany()
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Provider)
            .WithMany()
            .HasForeignKey(b => b.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);





    }
}
