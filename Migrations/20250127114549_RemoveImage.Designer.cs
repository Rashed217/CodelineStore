﻿// <auto-generated />
using System;
using CodelineStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodelineStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250127114549_RemoveImage")]
    partial class RemoveImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CodelineStore.Data.Model.Category", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("CatId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ClientId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Order", b =>
                {
                    b.Property<int>("OId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Product", b =>
                {
                    b.Property<int>("PId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("TotalSold")
                        .HasColumnType("int");

                    b.HasKey("PId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductImages", b =>
                {
                    b.Property<int>("imageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("imageId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("imagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("imageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductReport", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReportId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductReport");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductReview", b =>
                {
                    b.Property<int>("RId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Seller", b =>
                {
                    b.Property<int>("SId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SId"));

                    b.Property<string>("ProfileImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellerRating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Client", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("CodelineStore.Data.Model.Client", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Order", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Client", "client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.OrderDetail", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Order", "order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodelineStore.Data.Model.Product", "product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Product", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodelineStore.Data.Model.Seller", "Seller")
                        .WithMany("Products")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductImages", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Product", "product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductReport", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Client", "Client")
                        .WithMany("ProductReports")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodelineStore.Data.Model.Product", "Product")
                        .WithMany("ProductReports")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.ProductReview", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.Client", "Client")
                        .WithMany("ProductReviews")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodelineStore.Data.Model.Product", "Product")
                        .WithMany("ProductReviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Seller", b =>
                {
                    b.HasOne("CodelineStore.Data.Model.User", "User")
                        .WithOne("Seller")
                        .HasForeignKey("CodelineStore.Data.Model.Seller", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Client", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ProductReports");

                    b.Navigation("ProductReviews");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ProductReports");

                    b.Navigation("ProductReviews");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.Seller", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CodelineStore.Data.Model.User", b =>
                {
                    b.Navigation("Client")
                        .IsRequired();

                    b.Navigation("Seller")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
