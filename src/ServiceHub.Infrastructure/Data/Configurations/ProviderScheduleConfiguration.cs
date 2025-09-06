using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Infrastructure.Data.Configurations;

public class ProviderScheduleConfiguration : IEntityTypeConfiguration<ProviderSchedule>
{
    public void Configure(EntityTypeBuilder<ProviderSchedule> builder)
    {
        builder.ToTable("provider_schedules");
        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id)
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("id");

        builder.Property(ps => ps.ProviderId)
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasColumnName("provider_id");

        builder.Property(ps => ps.DayOfWeek)
            .HasColumnType("int")
            .IsRequired()
            .HasColumnName("day_of_week");

        builder.Property(ps => ps.StartTime)
            .HasColumnType("time")
            .IsRequired()
            .HasColumnName("start_time");

        builder.Property(ps => ps.EndTime)
            .HasColumnType("time")
            .IsRequired()
            .HasColumnName("end_time");

        builder.Property(ps => ps.IsActive)
            .HasColumnType("bit")
            .IsRequired()
            .HasColumnName("is_active");

        builder.HasOne(ps => ps.Provider)
            .WithMany()
            .HasForeignKey(ps => ps.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
