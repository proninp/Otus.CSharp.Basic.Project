﻿// <auto-generated />
using System;
using FinanceManager.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250102104000_CategoryEmoji")]
    partial class CategoryEmoji
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceManager.Core.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Emoji")
                        .HasColumnType("text");

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

                    b.Property<string>("Emoji")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("98cc6395-48f2-43bd-91f0-ee0d57fd8cce"),
                            CurrencyCode = "AED",
                            CurrencySign = "د.إ",
                            Emoji = "🇦🇪",
                            Title = "United Arab Emirates Dirham"
                        },
                        new
                        {
                            Id = new Guid("fe8021f5-e8cc-4a6c-9607-4f18032e595d"),
                            CurrencyCode = "AUD",
                            CurrencySign = "$",
                            Emoji = "🇦🇺",
                            Title = "Australian Dollar"
                        },
                        new
                        {
                            Id = new Guid("6610fd52-620f-4a1a-859a-3fe7dbd46e79"),
                            CurrencyCode = "BRL",
                            CurrencySign = "R$",
                            Emoji = "🇧🇷",
                            Title = "Brazilian Real"
                        },
                        new
                        {
                            Id = new Guid("43026e6c-c474-4bfd-be4b-b04a73ebf189"),
                            CurrencyCode = "CAD",
                            CurrencySign = "$",
                            Emoji = "🇨🇦",
                            Title = "Canadian Dollar"
                        },
                        new
                        {
                            Id = new Guid("811bd065-e06d-4de8-b483-e0f4c7a0e0bb"),
                            CurrencyCode = "CHF",
                            CurrencySign = "CHF",
                            Emoji = "🇨🇭",
                            Title = "Swiss Franc"
                        },
                        new
                        {
                            Id = new Guid("9ef4af3b-00ed-4f1c-8a45-0cc26c24c179"),
                            CurrencyCode = "CNY",
                            CurrencySign = "¥",
                            Emoji = "🇨🇳",
                            Title = "Chinese Yuan"
                        },
                        new
                        {
                            Id = new Guid("c03873e8-40a2-4df5-810d-ad7dc931042b"),
                            CurrencyCode = "DKK",
                            CurrencySign = "kr",
                            Emoji = "🇩🇰",
                            Title = "Danish Krone"
                        },
                        new
                        {
                            Id = new Guid("56c611cd-86e7-4e58-aa95-afb23677ed49"),
                            CurrencyCode = "EUR",
                            CurrencySign = "€",
                            Emoji = "🇪🇺",
                            Title = "Euro"
                        },
                        new
                        {
                            Id = new Guid("ec14cad1-becd-4dc0-b5e7-55b3699da688"),
                            CurrencyCode = "GBP",
                            CurrencySign = "£",
                            Emoji = "🇬🇧",
                            Title = "British Pound Sterling"
                        },
                        new
                        {
                            Id = new Guid("96da7787-414c-4064-9674-b9f542d58dfd"),
                            CurrencyCode = "HKD",
                            CurrencySign = "$",
                            Emoji = "🇭🇰",
                            Title = "Hong Kong Dollar"
                        },
                        new
                        {
                            Id = new Guid("47db743d-df24-426a-bce2-0d12d25548d0"),
                            CurrencyCode = "HUF",
                            CurrencySign = "Ft",
                            Emoji = "🇭🇺",
                            Title = "Hungarian Forint"
                        },
                        new
                        {
                            Id = new Guid("b21ec8dc-2b89-43da-91ba-eed792452c9a"),
                            CurrencyCode = "IDR",
                            CurrencySign = "Rp",
                            Emoji = "🇮🇩",
                            Title = "Indonesian Rupiah"
                        },
                        new
                        {
                            Id = new Guid("5a81a267-4e25-49f8-87ae-9c3321e0f5d1"),
                            CurrencyCode = "INR",
                            CurrencySign = "₹",
                            Emoji = "🇮🇳",
                            Title = "Indian Rupee"
                        },
                        new
                        {
                            Id = new Guid("8695e58f-3181-4f8f-bbb5-5e0b8f40c1b8"),
                            CurrencyCode = "JPY",
                            CurrencySign = "¥",
                            Emoji = "🇯🇵",
                            Title = "Japanese Yen"
                        },
                        new
                        {
                            Id = new Guid("ce1dab7b-ca6c-4ed1-b1f5-cb0ce36c95a7"),
                            CurrencyCode = "KRW",
                            CurrencySign = "₩",
                            Emoji = "🇰🇷",
                            Title = "South Korean Won"
                        },
                        new
                        {
                            Id = new Guid("3edcd445-24fc-4604-8e6e-b1c6293ccab4"),
                            CurrencyCode = "MXN",
                            CurrencySign = "$",
                            Emoji = "🇲🇽",
                            Title = "Mexican Peso"
                        },
                        new
                        {
                            Id = new Guid("f862fc99-8bb2-4c90-87bc-b66179f569d5"),
                            CurrencyCode = "MYR",
                            CurrencySign = "RM",
                            Emoji = "🇲🇾",
                            Title = "Malaysian Ringgit"
                        },
                        new
                        {
                            Id = new Guid("f6b96b2b-c10a-4e96-88c1-8be0aedb2084"),
                            CurrencyCode = "NOK",
                            CurrencySign = "kr",
                            Emoji = "🇳🇴",
                            Title = "Norwegian Krone"
                        },
                        new
                        {
                            Id = new Guid("d4ca36f1-2d5c-4da0-bfc2-eb0bcde3e13b"),
                            CurrencyCode = "NZD",
                            CurrencySign = "$",
                            Emoji = "🇳🇿",
                            Title = "New Zealand Dollar"
                        },
                        new
                        {
                            Id = new Guid("4b37252a-d88d-4d52-87b8-a2ba2d9dcbc0"),
                            CurrencyCode = "PHP",
                            CurrencySign = "₱",
                            Emoji = "🇵🇭",
                            Title = "Philippine Peso"
                        },
                        new
                        {
                            Id = new Guid("96d5b4a3-0c3d-4c9e-a31f-01652c3164e4"),
                            CurrencyCode = "PLN",
                            CurrencySign = "zł",
                            Emoji = "🇵🇱",
                            Title = "Polish Zloty"
                        },
                        new
                        {
                            Id = new Guid("e4767bf0-4c7b-4147-8db4-3d3821d2b105"),
                            CurrencyCode = "RUB",
                            CurrencySign = "₽",
                            Emoji = "🇷🇺",
                            Title = "Russian Ruble"
                        },
                        new
                        {
                            Id = new Guid("93256ae0-71bb-42c6-a040-dbad01aa4e67"),
                            CurrencyCode = "SEK",
                            CurrencySign = "kr",
                            Emoji = "🇸🇪",
                            Title = "Swedish Krona"
                        },
                        new
                        {
                            Id = new Guid("42ad145a-4c53-4a91-b7ec-a177ec2ea229"),
                            CurrencyCode = "SGD",
                            CurrencySign = "$",
                            Emoji = "🇸🇬",
                            Title = "Singapore Dollar"
                        },
                        new
                        {
                            Id = new Guid("e0ec27b6-1b27-44ca-a1b7-4e9c7474b87e"),
                            CurrencyCode = "THB",
                            CurrencySign = "฿",
                            Emoji = "🇹🇭",
                            Title = "Thai Baht"
                        },
                        new
                        {
                            Id = new Guid("7f696530-fcca-48d7-bdf2-2bec9ffd27fa"),
                            CurrencyCode = "TRY",
                            CurrencySign = "₺",
                            Emoji = "🇹🇷",
                            Title = "Turkish Lira"
                        },
                        new
                        {
                            Id = new Guid("22199448-255d-47ea-963b-8c32ceeb90c5"),
                            CurrencyCode = "USD",
                            CurrencySign = "$",
                            Emoji = "🇺🇸",
                            Title = "United States Dollar"
                        },
                        new
                        {
                            Id = new Guid("9ba31557-1867-4caf-8e7b-f166c6e30147"),
                            CurrencyCode = "ZAR",
                            CurrencySign = "R",
                            Emoji = "🇿🇦",
                            Title = "South African Rand"
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

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

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

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

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

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinanceManager.Core.Models.Account", b =>
                {
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
