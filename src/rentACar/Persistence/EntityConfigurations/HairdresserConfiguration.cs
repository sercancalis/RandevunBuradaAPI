using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
namespace Persistence.EntityConfigurations;

public class HairdresserConfiguration : IEntityTypeConfiguration<Hairdresser>
{
    public void Configure(EntityTypeBuilder<Hairdresser> builder)
    {
        builder.ToTable("Hairdressers").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.Latitude).HasColumnName("Latitude");
        builder.Property(p => p.Longitude).HasColumnName("Longitude");

        builder.HasIndex(indexExpression: p => p.Name, name: "UK_Hairdressers_Name").IsUnique();
    }
}