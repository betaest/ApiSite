﻿// <auto-generated />
using System;
using ApiSite.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiSite.Migrations.BillQuery
{
    [DbContext(typeof(BillQueryContext))]
    partial class BillQueryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApiSite.Models.BillQuery.Column", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Sortable");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("Width");

                    b.HasKey("Key");

                    b.ToTable("column");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.Connection", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionString")
                        .IsRequired();

                    b.HasKey("Name");

                    b.ToTable("Connection");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColumnKey")
                        .IsRequired();

                    b.Property<int?>("ResultId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ColumnKey");

                    b.HasIndex("ResultId");

                    b.ToTable("menu_item");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ResultId");

                    b.Property<int>("Type");

                    b.Property<int>("ValueId");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.HasIndex("ValueId");

                    b.ToTable("result");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionName");

                    b.Property<string>("Result")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ConnectionName");

                    b.ToTable("value");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.MenuItem", b =>
                {
                    b.HasOne("ApiSite.Models.BillQuery.Column", "Column")
                        .WithMany()
                        .HasForeignKey("ColumnKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiSite.Models.BillQuery.Result", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.Result", b =>
                {
                    b.HasOne("ApiSite.Models.BillQuery.Result")
                        .WithMany("Children")
                        .HasForeignKey("ResultId");

                    b.HasOne("ApiSite.Models.BillQuery.Value", "Value")
                        .WithMany()
                        .HasForeignKey("ValueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiSite.Models.BillQuery.Value", b =>
                {
                    b.HasOne("ApiSite.Models.BillQuery.Connection", "Connection")
                        .WithMany()
                        .HasForeignKey("ConnectionName");
                });
#pragma warning restore 612, 618
        }
    }
}