using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.BusinessId).HasColumnName("BusinessId");
        builder.Property(p => p.IsConfirmed).HasColumnName("IsConfirmed").HasDefaultValue<bool>(false);

        builder.HasOne(p => p.Business).WithMany(p => p.Employees).HasForeignKey(p => p.BusinessId).IsRequired();

        builder.HasIndex(indexExpression: p => p.UserId, name: "UK_Employees_UserId").IsUnique();
    }
}