using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CategoryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("22199448-255d-47ea-963b-8c32ceeb90c5"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3edcd445-24fc-4604-8e6e-b1c6293ccab4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("42ad145a-4c53-4a91-b7ec-a177ec2ea229"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("43026e6c-c474-4bfd-be4b-b04a73ebf189"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("47db743d-df24-426a-bce2-0d12d25548d0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4b37252a-d88d-4d52-87b8-a2ba2d9dcbc0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("56c611cd-86e7-4e58-aa95-afb23677ed49"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5a81a267-4e25-49f8-87ae-9c3321e0f5d1"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("6610fd52-620f-4a1a-859a-3fe7dbd46e79"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7f696530-fcca-48d7-bdf2-2bec9ffd27fa"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("811bd065-e06d-4de8-b483-e0f4c7a0e0bb"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("8695e58f-3181-4f8f-bbb5-5e0b8f40c1b8"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("93256ae0-71bb-42c6-a040-dbad01aa4e67"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("96d5b4a3-0c3d-4c9e-a31f-01652c3164e4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("96da7787-414c-4064-9674-b9f542d58dfd"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("98cc6395-48f2-43bd-91f0-ee0d57fd8cce"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9ba31557-1867-4caf-8e7b-f166c6e30147"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("9ef4af3b-00ed-4f1c-8a45-0cc26c24c179"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b21ec8dc-2b89-43da-91ba-eed792452c9a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c03873e8-40a2-4df5-810d-ad7dc931042b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ce1dab7b-ca6c-4ed1-b1f5-cb0ce36c95a7"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d4ca36f1-2d5c-4da0-bfc2-eb0bcde3e13b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("e0ec27b6-1b27-44ca-a1b7-4e9c7474b87e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("e4767bf0-4c7b-4147-8db4-3d3821d2b105"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ec14cad1-becd-4dc0-b5e7-55b3699da688"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f6b96b2b-c10a-4e96-88c1-8be0aedb2084"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f862fc99-8bb2-4c90-87bc-b66179f569d5"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("fe8021f5-e8cc-4a6c-9607-4f18032e595d"));

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Emoji", "Title" },
                values: new object[,]
                {
                    { new Guid("04fb7899-3ab7-4a4d-a222-507c24e17a70"), "NOK", "kr", "🇳🇴", "Norwegian Krone" },
                    { new Guid("07c3d415-eb4b-43e6-9751-28fbe90a0583"), "SGD", "$", "🇸🇬", "Singapore Dollar" },
                    { new Guid("07f621b7-e1f7-4412-bb30-455caa6f4f5f"), "KRW", "₩", "🇰🇷", "South Korean Won" },
                    { new Guid("1412f1df-2be7-402e-96f2-9d84aea83966"), "MYR", "RM", "🇲🇾", "Malaysian Ringgit" },
                    { new Guid("26a1f35d-184e-4c46-b0be-4de3e8f99faf"), "INR", "₹", "🇮🇳", "Indian Rupee" },
                    { new Guid("27d8037b-1cff-4ea6-88cd-46e8fc018895"), "IDR", "Rp", "🇮🇩", "Indonesian Rupiah" },
                    { new Guid("3e155dfb-d60d-487f-bbad-38891f319cea"), "CNY", "¥", "🇨🇳", "Chinese Yuan" },
                    { new Guid("45a9b0e7-51db-49ea-9a55-e78ed0438115"), "PLN", "zł", "🇵🇱", "Polish Zloty" },
                    { new Guid("48985362-384b-4c99-9e66-48c14c3cc4b0"), "ZAR", "R", "🇿🇦", "South African Rand" },
                    { new Guid("5682e4f4-d9b1-4516-b6b4-b476c8621283"), "CHF", "CHF", "🇨🇭", "Swiss Franc" },
                    { new Guid("6014164d-0804-4741-a112-1d91ef9d1743"), "HUF", "Ft", "🇭🇺", "Hungarian Forint" },
                    { new Guid("7b968b9b-67c2-4b6c-a1c7-3e6564fc9972"), "USD", "$", "🇺🇸", "United States Dollar" },
                    { new Guid("879499da-9274-4ec1-9396-9a7929fe2ea4"), "THB", "฿", "🇹🇭", "Thai Baht" },
                    { new Guid("88941148-96d5-4add-8d62-5d1816219e31"), "MXN", "$", "🇲🇽", "Mexican Peso" },
                    { new Guid("95e5625a-2513-494f-80ae-76eabd6814d2"), "DKK", "kr", "🇩🇰", "Danish Krone" },
                    { new Guid("a984694d-93b2-4dff-96a3-e3833e1d78f3"), "HKD", "$", "🇭🇰", "Hong Kong Dollar" },
                    { new Guid("ab70cc45-600b-414b-bce6-e25094891637"), "EUR", "€", "🇪🇺", "Euro" },
                    { new Guid("b01cd414-188b-4d3f-ba3b-d524baa7553c"), "GBP", "£", "🇬🇧", "British Pound Sterling" },
                    { new Guid("b10c89a3-3425-45c4-b1f5-7a2856b3fc75"), "AUD", "$", "🇦🇺", "Australian Dollar" },
                    { new Guid("b6dde2ef-44eb-4b60-8df4-3a0537b3e549"), "JPY", "¥", "🇯🇵", "Japanese Yen" },
                    { new Guid("ba66c4f8-7a03-48b2-bca8-6c97d574f92f"), "SEK", "kr", "🇸🇪", "Swedish Krona" },
                    { new Guid("c2c1ed50-aeeb-46e1-b9eb-370e07460e8e"), "TRY", "₺", "🇹🇷", "Turkish Lira" },
                    { new Guid("cb2683e4-77e6-4bf2-9785-b9cd256d6b0b"), "NZD", "$", "🇳🇿", "New Zealand Dollar" },
                    { new Guid("d37cb5ca-6709-4ee1-914c-c529cfb82117"), "PHP", "₱", "🇵🇭", "Philippine Peso" },
                    { new Guid("d8024aeb-a64b-4ea6-8ed6-032e6bd4e21c"), "CAD", "$", "🇨🇦", "Canadian Dollar" },
                    { new Guid("d8f9367f-5cd0-49dd-854f-08ccd738acd6"), "AED", "د.إ", "🇦🇪", "United Arab Emirates Dirham" },
                    { new Guid("e3fd0998-7859-4a8a-ac96-3a00f4a1380f"), "BRL", "R$", "🇧🇷", "Brazilian Real" },
                    { new Guid("fe2e42eb-cc6d-4a67-95df-6f19ebe84357"), "RUB", "₽", "🇷🇺", "Russian Ruble" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("04fb7899-3ab7-4a4d-a222-507c24e17a70"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("07c3d415-eb4b-43e6-9751-28fbe90a0583"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("07f621b7-e1f7-4412-bb30-455caa6f4f5f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1412f1df-2be7-402e-96f2-9d84aea83966"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("26a1f35d-184e-4c46-b0be-4de3e8f99faf"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("27d8037b-1cff-4ea6-88cd-46e8fc018895"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3e155dfb-d60d-487f-bbad-38891f319cea"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("45a9b0e7-51db-49ea-9a55-e78ed0438115"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("48985362-384b-4c99-9e66-48c14c3cc4b0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5682e4f4-d9b1-4516-b6b4-b476c8621283"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("6014164d-0804-4741-a112-1d91ef9d1743"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7b968b9b-67c2-4b6c-a1c7-3e6564fc9972"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("879499da-9274-4ec1-9396-9a7929fe2ea4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("88941148-96d5-4add-8d62-5d1816219e31"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("95e5625a-2513-494f-80ae-76eabd6814d2"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a984694d-93b2-4dff-96a3-e3833e1d78f3"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ab70cc45-600b-414b-bce6-e25094891637"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b01cd414-188b-4d3f-ba3b-d524baa7553c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b10c89a3-3425-45c4-b1f5-7a2856b3fc75"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b6dde2ef-44eb-4b60-8df4-3a0537b3e549"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ba66c4f8-7a03-48b2-bca8-6c97d574f92f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c2c1ed50-aeeb-46e1-b9eb-370e07460e8e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("cb2683e4-77e6-4bf2-9785-b9cd256d6b0b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d37cb5ca-6709-4ee1-914c-c529cfb82117"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d8024aeb-a64b-4ea6-8ed6-032e6bd4e21c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d8f9367f-5cd0-49dd-854f-08ccd738acd6"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("e3fd0998-7859-4a8a-ac96-3a00f4a1380f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("fe2e42eb-cc6d-4a67-95df-6f19ebe84357"));

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Emoji", "Title" },
                values: new object[,]
                {
                    { new Guid("22199448-255d-47ea-963b-8c32ceeb90c5"), "USD", "$", "🇺🇸", "United States Dollar" },
                    { new Guid("3edcd445-24fc-4604-8e6e-b1c6293ccab4"), "MXN", "$", "🇲🇽", "Mexican Peso" },
                    { new Guid("42ad145a-4c53-4a91-b7ec-a177ec2ea229"), "SGD", "$", "🇸🇬", "Singapore Dollar" },
                    { new Guid("43026e6c-c474-4bfd-be4b-b04a73ebf189"), "CAD", "$", "🇨🇦", "Canadian Dollar" },
                    { new Guid("47db743d-df24-426a-bce2-0d12d25548d0"), "HUF", "Ft", "🇭🇺", "Hungarian Forint" },
                    { new Guid("4b37252a-d88d-4d52-87b8-a2ba2d9dcbc0"), "PHP", "₱", "🇵🇭", "Philippine Peso" },
                    { new Guid("56c611cd-86e7-4e58-aa95-afb23677ed49"), "EUR", "€", "🇪🇺", "Euro" },
                    { new Guid("5a81a267-4e25-49f8-87ae-9c3321e0f5d1"), "INR", "₹", "🇮🇳", "Indian Rupee" },
                    { new Guid("6610fd52-620f-4a1a-859a-3fe7dbd46e79"), "BRL", "R$", "🇧🇷", "Brazilian Real" },
                    { new Guid("7f696530-fcca-48d7-bdf2-2bec9ffd27fa"), "TRY", "₺", "🇹🇷", "Turkish Lira" },
                    { new Guid("811bd065-e06d-4de8-b483-e0f4c7a0e0bb"), "CHF", "CHF", "🇨🇭", "Swiss Franc" },
                    { new Guid("8695e58f-3181-4f8f-bbb5-5e0b8f40c1b8"), "JPY", "¥", "🇯🇵", "Japanese Yen" },
                    { new Guid("93256ae0-71bb-42c6-a040-dbad01aa4e67"), "SEK", "kr", "🇸🇪", "Swedish Krona" },
                    { new Guid("96d5b4a3-0c3d-4c9e-a31f-01652c3164e4"), "PLN", "zł", "🇵🇱", "Polish Zloty" },
                    { new Guid("96da7787-414c-4064-9674-b9f542d58dfd"), "HKD", "$", "🇭🇰", "Hong Kong Dollar" },
                    { new Guid("98cc6395-48f2-43bd-91f0-ee0d57fd8cce"), "AED", "د.إ", "🇦🇪", "United Arab Emirates Dirham" },
                    { new Guid("9ba31557-1867-4caf-8e7b-f166c6e30147"), "ZAR", "R", "🇿🇦", "South African Rand" },
                    { new Guid("9ef4af3b-00ed-4f1c-8a45-0cc26c24c179"), "CNY", "¥", "🇨🇳", "Chinese Yuan" },
                    { new Guid("b21ec8dc-2b89-43da-91ba-eed792452c9a"), "IDR", "Rp", "🇮🇩", "Indonesian Rupiah" },
                    { new Guid("c03873e8-40a2-4df5-810d-ad7dc931042b"), "DKK", "kr", "🇩🇰", "Danish Krone" },
                    { new Guid("ce1dab7b-ca6c-4ed1-b1f5-cb0ce36c95a7"), "KRW", "₩", "🇰🇷", "South Korean Won" },
                    { new Guid("d4ca36f1-2d5c-4da0-bfc2-eb0bcde3e13b"), "NZD", "$", "🇳🇿", "New Zealand Dollar" },
                    { new Guid("e0ec27b6-1b27-44ca-a1b7-4e9c7474b87e"), "THB", "฿", "🇹🇭", "Thai Baht" },
                    { new Guid("e4767bf0-4c7b-4147-8db4-3d3821d2b105"), "RUB", "₽", "🇷🇺", "Russian Ruble" },
                    { new Guid("ec14cad1-becd-4dc0-b5e7-55b3699da688"), "GBP", "£", "🇬🇧", "British Pound Sterling" },
                    { new Guid("f6b96b2b-c10a-4e96-88c1-8be0aedb2084"), "NOK", "kr", "🇳🇴", "Norwegian Krone" },
                    { new Guid("f862fc99-8bb2-4c90-87bc-b66179f569d5"), "MYR", "RM", "🇲🇾", "Malaysian Ringgit" },
                    { new Guid("fe8021f5-e8cc-4a6c-9607-4f18032e595d"), "AUD", "$", "🇦🇺", "Australian Dollar" }
                });
        }
    }
}
