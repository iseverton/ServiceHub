using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

internal class ServiceReviewConfiguration : IEntityTypeConfiguration<ServiceReview>
{
    public void Configure(EntityTypeBuilder<ServiceReview> builder)
    {
         builder.ToTable("Service_reviews");
         builder.HasKey(sr => sr.Id);

        builder.Property(sr => sr.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id")
            .IsRequired();

        builder.Property(sr => sr.Comment)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(400)
            .HasColumnName("comment");

        builder.Property(sr => sr.Rating)
            .IsRequired()
            .HasColumnType("int")
            .HasColumnName("rating");

        builder.Property(sr => sr.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime")
            .HasColumnName("created_at");

        builder.Property(sr => sr.UpdatedAt)
            .HasColumnType("datetime")
            .HasColumnName("updated_at");

        builder.HasOne(sr => sr.Booking)
            .WithOne(b => b.ServiceReview)
           .HasForeignKey<ServiceReview>(sr => sr.BookingId);

        builder.HasOne(sr => sr.Service)
            .WithMany(s => s.ServiceReviews)
            .HasForeignKey(sr => sr.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.User)
            .WithMany(u => u.ServiceReviewss)
            .HasForeignKey(sr => sr.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sr => sr.Provider)
            .WithMany(p => p.ServiceReviews)
            .HasForeignKey(sr => sr.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
