﻿// <auto-generated />
using System;
using InventoryManagementSystem_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagementSystem_API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241016080455_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("InventoryManagementSystem_API.Product", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("cost_price")
                        .HasColumnType("REAL");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("profit_per_unit")
                        .HasColumnType("REAL");

                    b.Property<int>("quantity")
                        .HasColumnType("INTEGER");

                    b.Property<double>("selling_price")
                        .HasColumnType("REAL");

                    b.Property<int>("sold")
                        .HasColumnType("INTEGER");

                    b.Property<double>("total_profit")
                        .HasColumnType("REAL");

                    b.HasKey("id");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}