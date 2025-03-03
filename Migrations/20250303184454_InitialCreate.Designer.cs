﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleCatalog.API.Data;

#nullable disable

namespace VehicleCatalog.API.Migrations
{
    [DbContext(typeof(VehicleCatalogDbContext))]
    [Migration("20250303184454_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("VehicleCatalog.API.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Toyota"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ford"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Chevrolet"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Honda"
                        },
                        new
                        {
                            Id = 5,
                            Name = "BMW"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Mercedes-Benz"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Audi"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Nissan"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Hyundai"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Volkswagen"
                        });
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Image", b =>
                {
                    b.HasOne("VehicleCatalog.API.Models.Vehicle", "Vehicle")
                        .WithMany("Images")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Vehicle", b =>
                {
                    b.HasOne("VehicleCatalog.API.Models.Brand", "Brand")
                        .WithMany("Vehicles")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Brand", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("VehicleCatalog.API.Models.Vehicle", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
