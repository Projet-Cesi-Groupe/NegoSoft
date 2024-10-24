﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NegoSoftWeb.Data;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    [DbContext(typeof(NegoSoftContext))]
    [Migration("20241019111049_productFailure")]
    partial class productFailure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Address", b =>
                {
                    b.Property<Guid>("AddId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("add_id");

                    b.Property<string>("AddBillingCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_billing_city");

                    b.Property<string>("AddBillingCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_billing_country");

                    b.Property<string>("AddBillingStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_billing_street");

                    b.Property<string>("AddBillingZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_billing_zip_code");

                    b.Property<string>("AddDeliveryCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_delivery_city");

                    b.Property<string>("AddDeliveryCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_delivery_country");

                    b.Property<string>("AddDeliveryStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_delivery_street");

                    b.Property<string>("AddDeliveryZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("add_delivery_zip_code");

                    b.HasKey("AddId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Customer", b =>
                {
                    b.Property<Guid>("CusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cus_id");

                    b.Property<string>("CusEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cus_email");

                    b.Property<string>("CusFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cus_first_name");

                    b.Property<string>("CusLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cus_last_name");

                    b.Property<string>("CusPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cus_phone");

                    b.Property<string>("CusUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("cus_user_id");

                    b.HasKey("CusId");

                    b.HasIndex("CusUserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.CustomerOrder", b =>
                {
                    b.Property<Guid>("CoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("co_id");

                    b.Property<Guid>("CoAddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("co_address_id");

                    b.Property<Guid>("CoCustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("co_customer_id");

                    b.Property<DateTime>("CoDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("co_date");

                    b.Property<string>("CoState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("co_state");

                    b.Property<float>("CoTotal")
                        .HasColumnType("real")
                        .HasColumnName("co_total");

                    b.HasKey("CoId");

                    b.HasIndex("CoAddressId");

                    b.HasIndex("CoCustomerId");

                    b.ToTable("CustomerOrders");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.CustomerOrderDetails", b =>
                {
                    b.Property<Guid>("CodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cod_id");

                    b.Property<Guid>("CodOrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cod_order_id");

                    b.Property<float>("CodPrice")
                        .HasColumnType("real")
                        .HasColumnName("cod_price");

                    b.Property<Guid>("CodProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cod_product_id");

                    b.Property<int>("CodQuantity")
                        .HasColumnType("int")
                        .HasColumnName("cod_quantity");

                    b.HasKey("CodId");

                    b.HasIndex("CodOrderId");

                    b.HasIndex("CodProductId");

                    b.ToTable("CustomerOrderDetails");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Product", b =>
                {
                    b.Property<Guid>("ProId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("pro_id");

                    b.Property<float>("ProAlcoholVolume")
                        .HasColumnType("real")
                        .HasColumnName("pro_alcohol_volume");

                    b.Property<float?>("ProBoxPrice")
                        .HasColumnType("real")
                        .HasColumnName("pro_box_price");

                    b.Property<string>("ProDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pro_description");

                    b.Property<bool>("ProIsActive")
                        .HasColumnType("bit")
                        .HasColumnName("pro_is_active");

                    b.Property<string>("ProName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pro_name");

                    b.Property<string>("ProPictureName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pro_picture_name");

                    b.Property<float>("ProPrice")
                        .HasColumnType("real")
                        .HasColumnName("pro_price");

                    b.Property<int>("ProStock")
                        .HasColumnType("int")
                        .HasColumnName("pro_stock");

                    b.Property<Guid>("ProSupplierId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("pro_supplier_id");

                    b.Property<Guid>("ProTypeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("pro_type_id");

                    b.Property<int>("ProYear")
                        .HasColumnType("int")
                        .HasColumnName("pro_year");

                    b.HasKey("ProId");

                    b.HasIndex("ProSupplierId");

                    b.HasIndex("ProTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Supplier", b =>
                {
                    b.Property<Guid>("SupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sup_id");

                    b.Property<Guid>("SupDefaultAddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sup_default_address_id");

                    b.Property<string>("SupEmail")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sup_email");

                    b.Property<string>("SupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sup_name");

                    b.Property<string>("SupPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sup_phone");

                    b.HasKey("SupId");

                    b.HasIndex("SupDefaultAddressId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.SupplierOrder", b =>
                {
                    b.Property<Guid>("SoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("so_id");

                    b.Property<Guid>("SoAddressId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("so_address_id");

                    b.Property<DateTime>("SoDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("so_date");

                    b.Property<string>("SoState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("so_state");

                    b.Property<Guid>("SoSupplierId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("so_supplier_id");

                    b.Property<float>("SoTotal")
                        .HasColumnType("real")
                        .HasColumnName("so_total");

                    b.HasKey("SoId");

                    b.HasIndex("SoAddressId");

                    b.HasIndex("SoSupplierId");

                    b.ToTable("SupplierOrders");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.SupplierOrderDetails", b =>
                {
                    b.Property<Guid>("SodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sod_id");

                    b.Property<Guid>("SodOrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sod_order_id");

                    b.Property<float>("SodPrice")
                        .HasColumnType("real")
                        .HasColumnName("sod_price");

                    b.Property<Guid>("SodProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sod_product_id");

                    b.Property<int>("SodQuantity")
                        .HasColumnType("int")
                        .HasColumnName("sod_quantity");

                    b.HasKey("SodId");

                    b.HasIndex("SodOrderId");

                    b.HasIndex("SodProductId");

                    b.ToTable("SupplierOrderDetails");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Type", b =>
                {
                    b.Property<Guid>("TypId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("typ_id");

                    b.Property<string>("TypLibelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("typ_libelle");

                    b.HasKey("TypId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("NegoSoftWeb.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NegoSoftWeb.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NegoSoftWeb.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NegoSoftWeb.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NegoSoftWeb.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Customer", b =>
                {
                    b.HasOne("NegoSoftWeb.Models.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("CusUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.CustomerOrder", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.Address", "Address")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CoAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NegoSoftShared.Models.Entities.Customer", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CoCustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.CustomerOrderDetails", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.CustomerOrder", "CustomerOrder")
                        .WithMany("CustomerOrderDetails")
                        .HasForeignKey("CodOrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NegoSoftShared.Models.Entities.Product", "Product")
                        .WithMany("CustomerOrderDetails")
                        .HasForeignKey("CodProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CustomerOrder");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Product", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("ProSupplierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NegoSoftShared.Models.Entities.Type", "Type")
                        .WithMany("Products")
                        .HasForeignKey("ProTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Supplier", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.Address", "DefaultAddress")
                        .WithMany("Suppliers")
                        .HasForeignKey("SupDefaultAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DefaultAddress");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.SupplierOrder", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.Address", "Address")
                        .WithMany("SupplierOrders")
                        .HasForeignKey("SoAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NegoSoftShared.Models.Entities.Supplier", "Supplier")
                        .WithMany("SupplierOrders")
                        .HasForeignKey("SoSupplierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.SupplierOrderDetails", b =>
                {
                    b.HasOne("NegoSoftShared.Models.Entities.SupplierOrder", "SupplierOrder")
                        .WithMany("SupplierOrderDetails")
                        .HasForeignKey("SodOrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NegoSoftShared.Models.Entities.Product", "Product")
                        .WithMany("SupplierOrderDetails")
                        .HasForeignKey("SodProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SupplierOrder");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Address", b =>
                {
                    b.Navigation("CustomerOrders");

                    b.Navigation("SupplierOrders");

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Customer", b =>
                {
                    b.Navigation("CustomerOrders");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.CustomerOrder", b =>
                {
                    b.Navigation("CustomerOrderDetails");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Product", b =>
                {
                    b.Navigation("CustomerOrderDetails");

                    b.Navigation("SupplierOrderDetails");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Supplier", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SupplierOrders");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.SupplierOrder", b =>
                {
                    b.Navigation("SupplierOrderDetails");
                });

            modelBuilder.Entity("NegoSoftShared.Models.Entities.Type", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
