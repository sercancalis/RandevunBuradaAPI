using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
{
    public void Configure(EntityTypeBuilder<UserDevice> builder)
    {
        builder.ToTable("UserDevices").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.DeviceToken).HasColumnName("DeviceToken");
        builder.Property(p => p.Brand).HasColumnName("Brand");
        builder.Property(p => p.DeviceName).HasColumnName("DeviceName");
        builder.Property(p => p.DeviceType).HasColumnName("DeviceType");
        builder.Property(p => p.ModelName).HasColumnName("ModelName");
        builder.Property(p => p.OsVersion).HasColumnName("OsVersion"); 

        builder.HasIndex(indexExpression: p => p.UserId, name: "UK_UserDevices_UserId");
    }
}