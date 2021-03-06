// <auto-generated />
using System;
using CafeAdmin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CafeAdmin.Migrations
{
    [DbContext(typeof(CafeAdminDbContext))]
    [Migration("20210801205750_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Cafe.Models.ClientTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WaiterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WaiterId");

                    b.ToTable("ClientTables");
                });

            modelBuilder.Entity("Cafe.Models.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFood")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("Cafe.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TableId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Cafe.Models.OrdersGoods", b =>
                {
                    b.Property<int>("GoodId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrdersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsServed")
                        .HasColumnType("INTEGER");

                    b.HasKey("GoodId", "OrdersId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("OrdersId");

                    b.ToTable("OrdersGoods");
                });

            modelBuilder.Entity("Cafe.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            Password = "12345"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ivan Gardin",
                            Password = "12345"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Petro Stepanov",
                            Password = "12345"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Mirko Shuher",
                            Password = "12345"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Peter Hugert",
                            Password = "12345"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Ruzhena Stefanic",
                            Password = "12345"
                        });
                });

            modelBuilder.Entity("Cafe.Models.UserAccesLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccesLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessLevel = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccessLevel = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            AccessLevel = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            AccessLevel = 2,
                            UserId = 4
                        },
                        new
                        {
                            Id = 5,
                            AccessLevel = 3,
                            UserId = 5
                        },
                        new
                        {
                            Id = 6,
                            AccessLevel = 3,
                            UserId = 6
                        },
                        new
                        {
                            Id = 7,
                            AccessLevel = 2,
                            UserId = 6
                        });
                });

            modelBuilder.Entity("Cafe.Models.ClientTable", b =>
                {
                    b.HasOne("Cafe.Models.User", "Waiter")
                        .WithMany()
                        .HasForeignKey("WaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Waiter");
                });

            modelBuilder.Entity("Cafe.Models.Orders", b =>
                {
                    b.HasOne("Cafe.Models.ClientTable", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Cafe.Models.OrdersGoods", b =>
                {
                    b.HasOne("Cafe.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Cafe.Models.Goods", "Good")
                        .WithMany("OrdersGoods")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafe.Models.Orders", "Orders")
                        .WithMany("OrdersGoods")
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Good");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Cafe.Models.UserAccesLevel", b =>
                {
                    b.HasOne("Cafe.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cafe.Models.Goods", b =>
                {
                    b.Navigation("OrdersGoods");
                });

            modelBuilder.Entity("Cafe.Models.Orders", b =>
                {
                    b.Navigation("OrdersGoods");
                });
#pragma warning restore 612, 618
        }
    }
}
