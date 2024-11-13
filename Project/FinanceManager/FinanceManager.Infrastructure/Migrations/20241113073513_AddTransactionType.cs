using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("4b112d03-f439-413d-8f26-f6ac0361f25b"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("978e7552-59f0-4a8d-81a3-7ef67c0f4762"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("cc7f5623-d592-4229-875b-f957833a1f07"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("f1a07ccc-d9fa-40d7-8482-2ef813bd01a8"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("ff898dca-a3bd-4ffc-8ed4-b19f1c239cca"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("048f5108-c044-4df0-88aa-7db21e5f2aca"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("12124374-9e9b-4f96-aa04-15c20151afd9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("29972ee4-5c83-4598-8ea7-40a2be44326b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("41191804-0a57-4f34-98c5-8e78668dfb23"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5bfc35fd-9e17-4402-866b-044e6654ea0d"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("dcc3d13c-383e-4405-a844-bd99fae3d2bf"));

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5bd53f87-759d-4a5d-bf9e-aa1bbe4a4d81"), "Cash" },
                    { new Guid("607f7e15-f7be-4264-b13b-24566f1c5644"), "Checking" },
                    { new Guid("dfc59c8a-9f9a-4c7f-b876-df76bf0cd66a"), "Deposit" },
                    { new Guid("eca18e12-774f-41b5-904d-21c0479a4e3a"), "Loan" },
                    { new Guid("fd327f80-48c9-4fa0-8aa8-2da2dd14baa0"), "Debit/credit card" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Title" },
                values: new object[,]
                {
                    { new Guid("132c0671-36e4-48ca-86be-683ec0aa021e"), "RUB", "₽", "Russian Ruble" },
                    { new Guid("32c7d5aa-7d44-4be2-8568-d869f9d8b0e8"), "BYN", "Br", "Belarusian Ruble" },
                    { new Guid("3c1bac58-06ab-4031-a942-1a6f42ea4f1f"), "TRY", "₺", "Turkish Lira" },
                    { new Guid("559da97b-f4b7-4be4-b3fa-1b74f055be2e"), "USD", "$", "United States Dollar" },
                    { new Guid("c3dce05d-e7b5-4e45-a269-40933c14cc61"), "GBP", "£", "British Pound Sterling" },
                    { new Guid("ed4a9df2-0461-4095-971e-96dfefeb3f9c"), "EUR", "€", "Euro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("5bd53f87-759d-4a5d-bf9e-aa1bbe4a4d81"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("607f7e15-f7be-4264-b13b-24566f1c5644"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("dfc59c8a-9f9a-4c7f-b876-df76bf0cd66a"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("eca18e12-774f-41b5-904d-21c0479a4e3a"));

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("fd327f80-48c9-4fa0-8aa8-2da2dd14baa0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("132c0671-36e4-48ca-86be-683ec0aa021e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("32c7d5aa-7d44-4be2-8568-d869f9d8b0e8"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3c1bac58-06ab-4031-a942-1a6f42ea4f1f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("559da97b-f4b7-4be4-b3fa-1b74f055be2e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c3dce05d-e7b5-4e45-a269-40933c14cc61"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ed4a9df2-0461-4095-971e-96dfefeb3f9c"));

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4b112d03-f439-413d-8f26-f6ac0361f25b"), "Loan" },
                    { new Guid("978e7552-59f0-4a8d-81a3-7ef67c0f4762"), "Checking" },
                    { new Guid("cc7f5623-d592-4229-875b-f957833a1f07"), "Debit/credit card" },
                    { new Guid("f1a07ccc-d9fa-40d7-8482-2ef813bd01a8"), "Deposit" },
                    { new Guid("ff898dca-a3bd-4ffc-8ed4-b19f1c239cca"), "Cash" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Title" },
                values: new object[,]
                {
                    { new Guid("048f5108-c044-4df0-88aa-7db21e5f2aca"), "USD", "$", "United States Dollar" },
                    { new Guid("12124374-9e9b-4f96-aa04-15c20151afd9"), "TRY", "₺", "Turkish Lira" },
                    { new Guid("29972ee4-5c83-4598-8ea7-40a2be44326b"), "EUR", "€", "Euro" },
                    { new Guid("41191804-0a57-4f34-98c5-8e78668dfb23"), "GBP", "£", "British Pound Sterling" },
                    { new Guid("5bfc35fd-9e17-4402-866b-044e6654ea0d"), "RUB", "₽", "Russian Ruble" },
                    { new Guid("dcc3d13c-383e-4405-a844-bd99fae3d2bf"), "BYN", "Br", "Belarusian Ruble" }
                });
        }
    }
}
