﻿using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Title).HasColumnName("Title");
        builder.Property(p => p.Body).HasColumnName("Body");
        builder.Property(p => p.SenderId).HasColumnName("SenderId");
        builder.Property(p => p.ReceiverId).HasColumnName("ReceiverId");
        builder.Property(p => p.NotificationType).HasColumnName("NotificationType");

        builder.HasIndex(indexExpression: p => p.ReceiverId, name: "UK_Notifications_ReceiverId");
    }
}
