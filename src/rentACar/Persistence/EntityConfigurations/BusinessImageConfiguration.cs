using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BusinessImageConfiguration : IEntityTypeConfiguration<BusinessImage>
{
    public void Configure(EntityTypeBuilder<BusinessImage> builder)
    {
        builder.ToTable("BusinessImages").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.BusinessId).HasColumnName("BusinessId");
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

        builder.HasOne(p => p.Business).WithMany(p => p.BusinessImages).HasForeignKey(p => p.BusinessId).IsRequired();

        builder.HasIndex(indexExpression: p => p.BusinessId, name: "UK_BusinessImages_BusinessId");
    }
}