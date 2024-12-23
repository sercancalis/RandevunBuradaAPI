using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BusinessServiceConfiguration : IEntityTypeConfiguration<BusinessService>
{
    public void Configure(EntityTypeBuilder<BusinessService> builder)
    {
        builder.ToTable("BusinessServices").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.BusinessId).HasColumnName("BusinessId");
        builder.Property(p => p.Name).HasColumnName("Name");

        builder.HasOne(p => p.Business).WithMany(p => p.BusinessServices).HasForeignKey(p => p.BusinessId).IsRequired();

        builder.HasIndex(indexExpression: p => p.BusinessId, name: "UK_BusinessServices_BusinessId");
    }
}