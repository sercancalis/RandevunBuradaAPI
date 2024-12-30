using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointments").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.PersonelId).HasColumnName("PersonelId");
        builder.Property(p => p.Date).HasColumnName("Date");
        builder.Property(p => p.Time).HasColumnName("Time");
        builder.Property(p => p.Services).HasColumnName("Services"); 
        builder.Property(p => p.IsConfirmed).HasColumnName("IsConfirmed").HasDefaultValue<bool>(false);

        builder.HasOne(p => p.Business).WithMany(p => p.Appointments).HasForeignKey(p => p.BusinessId);

        builder.HasIndex(indexExpression: p => p.BusinessId, name: "UK_Appointments_BusinessId");
    }
}


