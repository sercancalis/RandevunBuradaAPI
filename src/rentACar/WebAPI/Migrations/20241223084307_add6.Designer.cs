﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20241223084307_add6")]
    partial class add6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Category");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("City");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("District");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsConfirmed");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Latitude");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "UK_UserId");

                    b.ToTable("Businesses", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BusinessImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int")
                        .HasColumnName("BusinessId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImageUrl");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BusinessId" }, "UK_BusinessImages_BusinessId");

                    b.ToTable("BusinessImages", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BusinessService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int")
                        .HasColumnName("BusinessId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BusinessId" }, "UK_BusinessServices_BusinessId");

                    b.ToTable("BusinessServices", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int")
                        .HasColumnName("BusinessId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsConfirmed");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex(new[] { "UserId" }, "UK_Employees_UserId")
                        .IsUnique();

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int")
                        .HasColumnName("NotificationType");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ReceiverId");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SenderId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ReceiverId" }, "UK_Notifications_ReceiverId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Brand");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeviceName");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeviceToken");

                    b.Property<string>("DeviceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeviceType");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ModelName");

                    b.Property<string>("OsVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OsVersion");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "UK_UserDevices_UserId");

                    b.ToTable("UserDevices", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.WorkingHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int")
                        .HasColumnName("BusinessId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Value");

                    b.Property<int>("WorkingDay")
                        .HasColumnType("int")
                        .HasColumnName("WorkingDay");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BusinessId" }, "UK_WorkingHours_BusinessId");

                    b.ToTable("WorkingHours", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BusinessImage", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("BusinessImages")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.BusinessService", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("BusinessServices")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("Employees")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.WorkingHour", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("WorkingHours")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Business", b =>
                {
                    b.Navigation("BusinessImages");

                    b.Navigation("BusinessServices");

                    b.Navigation("Employees");

                    b.Navigation("WorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
