﻿// <auto-generated />
using System;
using HousingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HousingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230929074953_SecondMigration")]
    partial class SecondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HousingSystem.Models.Flat", b =>
                {
                    b.Property<string>("FlatNo")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int>("SqFeetArea")
                        .HasColumnType("int");

                    b.HasKey("FlatNo");

                    b.ToTable("Flat");
                });

            modelBuilder.Entity("HousingSystem.Models.Maintenance", b =>
                {
                    b.Property<string>("MaintenanceId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FlatNo")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("MaintenanceAccountHead")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MaintenanceAmount")
                        .HasColumnType("real");

                    b.Property<DateTime>("MaintenanceMonthYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaintenanceRemark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccupantId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.HasKey("MaintenanceId");

                    b.HasIndex("FlatNo")
                        .IsUnique();

                    b.HasIndex("OccupantId");

                    b.ToTable("Maintenance");
                });

            modelBuilder.Entity("HousingSystem.Models.Occupant", b =>
                {
                    b.Property<string>("OccupantId")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("FlatNo")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<DateTime>("OccupantEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OccupantFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OccupantIsOwned")
                        .HasColumnType("bit");

                    b.Property<string>("OccupantLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccupantPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("OccupantStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.HasKey("OccupantId");

                    b.HasIndex("FlatNo")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Occupant");
                });

            modelBuilder.Entity("HousingSystem.Models.Owner", b =>
                {
                    b.Property<string>("OwnerId")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("FlatNo")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<DateTime>("OwnerDateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnerEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("OwnerId");

                    b.HasIndex("FlatNo")
                        .IsUnique();

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("HousingSystem.Models.Payment", b =>
                {
                    b.Property<string>("PaymentId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("OccupantId")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<string>("PaymentAccountHead")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentReceiptId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentRemarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentTransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("OccupantId")
                        .IsUnique();

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("HousingSystem.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccupantId")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("UserActive")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OccupantId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("HousingSystem.Models.Maintenance", b =>
                {
                    b.HasOne("HousingSystem.Models.Flat", "Flat")
                        .WithOne("Maintenance")
                        .HasForeignKey("HousingSystem.Models.Maintenance", "FlatNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousingSystem.Models.Occupant", "Occupant")
                        .WithMany("Maintenances")
                        .HasForeignKey("OccupantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flat");

                    b.Navigation("Occupant");
                });

            modelBuilder.Entity("HousingSystem.Models.Occupant", b =>
                {
                    b.HasOne("HousingSystem.Models.Flat", "Flat")
                        .WithOne("Occupant")
                        .HasForeignKey("HousingSystem.Models.Occupant", "FlatNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousingSystem.Models.Owner", "Owner")
                        .WithMany("Occupants")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flat");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("HousingSystem.Models.Owner", b =>
                {
                    b.HasOne("HousingSystem.Models.Flat", "Flat")
                        .WithOne("Owner")
                        .HasForeignKey("HousingSystem.Models.Owner", "FlatNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flat");
                });

            modelBuilder.Entity("HousingSystem.Models.Payment", b =>
                {
                    b.HasOne("HousingSystem.Models.Occupant", "Occupant")
                        .WithOne("Payment")
                        .HasForeignKey("HousingSystem.Models.Payment", "OccupantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Occupant");
                });

            modelBuilder.Entity("HousingSystem.Models.User", b =>
                {
                    b.HasOne("HousingSystem.Models.Occupant", "Occupant")
                        .WithOne("User")
                        .HasForeignKey("HousingSystem.Models.User", "OccupantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Occupant");
                });

            modelBuilder.Entity("HousingSystem.Models.Flat", b =>
                {
                    b.Navigation("Maintenance");

                    b.Navigation("Occupant");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("HousingSystem.Models.Occupant", b =>
                {
                    b.Navigation("Maintenances");

                    b.Navigation("Payment")
                        .IsRequired();

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("HousingSystem.Models.Owner", b =>
                {
                    b.Navigation("Occupants");
                });
#pragma warning restore 612, 618
        }
    }
}
