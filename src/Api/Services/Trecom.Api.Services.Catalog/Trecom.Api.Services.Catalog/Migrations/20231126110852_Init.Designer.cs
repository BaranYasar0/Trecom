﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework;

#nullable disable

namespace Trecom.Api.Services.Catalog.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20231126110852_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("catalog")
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.BaseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.ToTable("BaseCategories", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.ToTable("Brands", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BaseCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BodyType")
                        .HasColumnType("int");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Color")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NAME");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("PICURL");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.HasIndex("BaseCategoryId");

                    b.HasIndex("BrandId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.SpecificCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("TypeCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.HasIndex("TypeCategoryId");

                    b.ToTable("SpecificCategories", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("BillStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.ToTable("Suppliers", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.TypeCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BaseCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("ModifyDate");

                    b.HasKey("Id");

                    b.HasIndex("BaseCategoryId");

                    b.ToTable("TypeCategories", "catalog");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.Product", b =>
                {
                    b.HasOne("Trecom.Api.Services.Catalog.Models.Entities.BaseCategory", "BaseCategory")
                        .WithMany()
                        .HasForeignKey("BaseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trecom.Api.Services.Catalog.Models.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trecom.Api.Services.Catalog.Models.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseCategory");

                    b.Navigation("Brand");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.SpecificCategory", b =>
                {
                    b.HasOne("Trecom.Api.Services.Catalog.Models.Entities.TypeCategory", "TypeCategory")
                        .WithMany()
                        .HasForeignKey("TypeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeCategory");
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.Supplier", b =>
                {
                    b.OwnsOne("Trecom.Api.Services.Catalog.Models.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("SupplierId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SupplierId");

                            b1.ToTable("Suppliers", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("SupplierId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Trecom.Api.Services.Catalog.Models.Entities.TypeCategory", b =>
                {
                    b.HasOne("Trecom.Api.Services.Catalog.Models.Entities.BaseCategory", "BaseCategory")
                        .WithMany()
                        .HasForeignKey("BaseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
