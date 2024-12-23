using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
{
    public void Configure(EntityTypeBuilder<WorkingHour> builder)
    {
        builder.ToTable("WorkingHours").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.WorkingDay).HasColumnName("WorkingDay");
        builder.Property(p => p.BusinessId).HasColumnName("BusinessId");
        builder.Property(p => p.Value).HasColumnName("Value");

        builder.HasOne(p => p.Business).WithMany(p => p.WorkingHours).HasForeignKey(p => p.BusinessId).IsRequired();

        builder.HasIndex(indexExpression: p => p.BusinessId, name: "UK_WorkingHours_BusinessId"); 
    }
}