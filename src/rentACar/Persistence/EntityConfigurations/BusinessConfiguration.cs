using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("Businesses").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.Latitude).HasColumnName("Latitude");
        builder.Property(p => p.Longitude).HasColumnName("Longitude");
        builder.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(p => p.City).HasColumnName("City");
        builder.Property(p => p.District).HasColumnName("District");
        builder.Property(p => p.Address).HasColumnName("Address");
        builder.Property(p => p.IsConfirmed).HasColumnName("IsConfirmed").HasDefaultValue<bool>(false);

        builder.HasMany(p => p.Employees).WithOne(p => p.Business).HasForeignKey(p => p.BusinessId).IsRequired();
        builder.HasMany(p => p.BusinessImages).WithOne(p => p.Business).HasForeignKey(p => p.BusinessId).IsRequired();
        builder.HasMany(p => p.WorkingHours).WithOne(p => p.Business).HasForeignKey(p => p.BusinessId).IsRequired();

        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Businesses_Name");
    }
}
