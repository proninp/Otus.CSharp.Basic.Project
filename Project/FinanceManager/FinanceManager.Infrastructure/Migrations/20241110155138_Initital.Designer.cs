﻿// <auto-generated />
using System;
using FinanceManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241110155138_Initital")]
    partial class Initital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceManager.Core.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountTypeId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.AccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ff898dca-a3bd-4ffc-8ed4-b19f1c239cca"),
                            Name = "Cash"
                        },
                        new
                        {
                            Id = new Guid("cc7f5623-d592-4229-875b-f957833a1f07"),
                            Name = "Debit/credit card"
                        },
                        new
                        {
                            Id = new Guid("978e7552-59f0-4a8d-81a3-7ef67c0f4762"),
                            Name = "Checking"
                        },
                        new
                        {
                            Id = new Guid("4b112d03-f439-413d-8f26-f6ac0361f25b"),
                            Name = "Loan"
                        },
                        new
                        {
                            Id = new Guid("f1a07ccc-d9fa-40d7-8482-2ef813bd01a8"),
                            Name = "Deposit"
                        });
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CurrencySign")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5bfc35fd-9e17-4402-866b-044e6654ea0d"),
                            CurrencyCode = "RUB",
                            CurrencySign = "₽",
                            Title = "Russian Ruble"
                        },
                        new
                        {
                            Id = new Guid("dcc3d13c-383e-4405-a844-bd99fae3d2bf"),
                            CurrencyCode = "BYN",
                            CurrencySign = "Br",
                            Title = "Belarusian Ruble"
                        },
                        new
                        {
                            Id = new Guid("048f5108-c044-4df0-88aa-7db21e5f2aca"),
                            CurrencyCode = "USD",
                            CurrencySign = "$",
                            Title = "United States Dollar"
                        },
                        new
                        {
                            Id = new Guid("29972ee4-5c83-4598-8ea7-40a2be44326b"),
                            CurrencyCode = "EUR",
                            CurrencySign = "€",
                            Title = "Euro"
                        },
                        new
                        {
                            Id = new Guid("41191804-0a57-4f34-98c5-8e78668dfb23"),
                            CurrencyCode = "GBP",
                            CurrencySign = "£",
                            Title = "British Pound Sterling"
                        },
                        new
                        {
                            Id = new Guid("12124374-9e9b-4f96-aa04-15c20151afd9"),
                            CurrencyCode = "TRY",
                            CurrencySign = "₺",
                            Title = "Turkish Lira"
                        });
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("FromAccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("FromAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ToAccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ToAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Account", b =>
                {
                    b.HasOne("FinanceManager.Core.Models.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.User", null)
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountType");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Category", b =>
                {
                    b.HasOne("FinanceManager.Core.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FinanceManager.Core.Models.User", null)
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Transaction", b =>
                {
                    b.HasOne("FinanceManager.Core.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Transfer", b =>
                {
                    b.HasOne("FinanceManager.Core.Models.Account", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.Account", "ToAccount")
                        .WithMany()
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManager.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Categories");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
