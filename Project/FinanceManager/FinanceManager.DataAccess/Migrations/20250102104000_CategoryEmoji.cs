using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CategoryEmoji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0952a1a0-0f14-4baf-9d34-bc35768e1ded"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0e5ffadf-6827-4fe3-8de7-8c26e322cc70"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("0e88027d-f0d1-4720-8bfb-cb57d9d406f6"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1174bcb4-b2fc-4750-8715-929fab5e18f0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("151be0e9-1b46-41f4-85d2-cc928be02538"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1732f57b-f1f1-42ab-9ee6-6463efa212d8"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("1f47bb5c-8898-4387-bdc8-66d60a04ed18"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("345665a8-5782-4c69-9d23-b9e89f02b2b0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("3974e202-ceca-42d8-83ea-29a4dc73d70c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4c0271a4-b456-4238-bcc9-0b8d5d4ef515"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("73858c30-5c2d-46e5-af21-b8bd817069e9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("859482f6-2873-425d-829c-6b2c0436d3fd"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("91f5a620-7d5c-4a07-9378-2ae34f3118e9"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("99e74ad8-4ae1-4ab4-8de0-f7ce3b596866"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ad9261a5-0c94-4277-8cd6-37b9add03700"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b6afe66d-5121-4d3f-bf73-81966a13bae1"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b6ea691b-6412-4968-bbfc-454b07eeae33"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b87ed1ac-cc12-43d2-94cd-92c4c77b9dce"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b8bf10f0-9e33-41e8-9d07-f8c650a17b85"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b91e0e17-a8e6-48dc-8c5a-e0cc4b776226"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b9265e4a-d203-4af7-a304-c111f7d3c6af"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c72b9423-eb2a-4044-970b-f04a2d587606"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c8b0a154-c79c-411c-9834-ba765794b4a7"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d2898652-3353-4bb5-8abb-46de806db403"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d7b4ea3b-5a9e-47b5-8fc0-6f5b7ffc9209"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("dc40ff04-9888-4130-a12a-aee7b404bd6b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f268886a-6962-47f8-837c-f5ce77fffa0c"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("fcece1e0-56d9-402b-bb87-8cf51c49f355"));

            migrationBuilder.AddColumn<string>(
                name: "Emoji",
                table: "Categories",
                type: "text",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Emoji",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Emoji", "Title" },
                values: new object[,]
                {
                    { new Guid("0952a1a0-0f14-4baf-9d34-bc35768e1ded"), "PLN", "zł", "🇵🇱", "Polish Zloty" },
                    { new Guid("0e5ffadf-6827-4fe3-8de7-8c26e322cc70"), "IDR", "Rp", "🇮🇩", "Indonesian Rupiah" },
                    { new Guid("0e88027d-f0d1-4720-8bfb-cb57d9d406f6"), "HKD", "$", "🇭🇰", "Hong Kong Dollar" },
                    { new Guid("1174bcb4-b2fc-4750-8715-929fab5e18f0"), "MXN", "$", "🇲🇽", "Mexican Peso" },
                    { new Guid("151be0e9-1b46-41f4-85d2-cc928be02538"), "BRL", "R$", "🇧🇷", "Brazilian Real" },
                    { new Guid("1732f57b-f1f1-42ab-9ee6-6463efa212d8"), "HUF", "Ft", "🇭🇺", "Hungarian Forint" },
                    { new Guid("1f47bb5c-8898-4387-bdc8-66d60a04ed18"), "NZD", "$", "🇳🇿", "New Zealand Dollar" },
                    { new Guid("345665a8-5782-4c69-9d23-b9e89f02b2b0"), "SEK", "kr", "🇸🇪", "Swedish Krona" },
                    { new Guid("3974e202-ceca-42d8-83ea-29a4dc73d70c"), "NOK", "kr", "🇳🇴", "Norwegian Krone" },
                    { new Guid("4c0271a4-b456-4238-bcc9-0b8d5d4ef515"), "USD", "$", "🇺🇸", "United States Dollar" },
                    { new Guid("73858c30-5c2d-46e5-af21-b8bd817069e9"), "THB", "฿", "🇹🇭", "Thai Baht" },
                    { new Guid("859482f6-2873-425d-829c-6b2c0436d3fd"), "AUD", "$", "🇦🇺", "Australian Dollar" },
                    { new Guid("91f5a620-7d5c-4a07-9378-2ae34f3118e9"), "CHF", "CHF", "🇨🇭", "Swiss Franc" },
                    { new Guid("99e74ad8-4ae1-4ab4-8de0-f7ce3b596866"), "GBP", "£", "🇬🇧", "British Pound Sterling" },
                    { new Guid("ad9261a5-0c94-4277-8cd6-37b9add03700"), "CNY", "¥", "🇨🇳", "Chinese Yuan" },
                    { new Guid("b6afe66d-5121-4d3f-bf73-81966a13bae1"), "CAD", "$", "🇨🇦", "Canadian Dollar" },
                    { new Guid("b6ea691b-6412-4968-bbfc-454b07eeae33"), "AED", "د.إ", "🇦🇪", "United Arab Emirates Dirham" },
                    { new Guid("b87ed1ac-cc12-43d2-94cd-92c4c77b9dce"), "RUB", "₽", "🇷🇺", "Russian Ruble" },
                    { new Guid("b8bf10f0-9e33-41e8-9d07-f8c650a17b85"), "MYR", "RM", "🇲🇾", "Malaysian Ringgit" },
                    { new Guid("b91e0e17-a8e6-48dc-8c5a-e0cc4b776226"), "DKK", "kr", "🇩🇰", "Danish Krone" },
                    { new Guid("b9265e4a-d203-4af7-a304-c111f7d3c6af"), "JPY", "¥", "🇯🇵", "Japanese Yen" },
                    { new Guid("c72b9423-eb2a-4044-970b-f04a2d587606"), "ZAR", "R", "🇿🇦", "South African Rand" },
                    { new Guid("c8b0a154-c79c-411c-9834-ba765794b4a7"), "TRY", "₺", "🇹🇷", "Turkish Lira" },
                    { new Guid("d2898652-3353-4bb5-8abb-46de806db403"), "INR", "₹", "🇮🇳", "Indian Rupee" },
                    { new Guid("d7b4ea3b-5a9e-47b5-8fc0-6f5b7ffc9209"), "EUR", "€", "🇪🇺", "Euro" },
                    { new Guid("dc40ff04-9888-4130-a12a-aee7b404bd6b"), "KRW", "₩", "🇰🇷", "South Korean Won" },
                    { new Guid("f268886a-6962-47f8-837c-f5ce77fffa0c"), "PHP", "₱", "🇵🇭", "Philippine Peso" },
                    { new Guid("fcece1e0-56d9-402b-bb87-8cf51c49f355"), "SGD", "$", "🇸🇬", "Singapore Dollar" }
                });
        }
    }
}
