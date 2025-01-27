using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeCategoryIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CurrencyCode", "CurrencySign", "Emoji", "Title" },
                values: new object[,]
                {
                    { new Guid("01805c1a-e33b-463d-a02c-28896bbe52d0"), "RUB", "₽", "🇷🇺", "Russian Ruble" },
                    { new Guid("050c49ba-4ba4-422b-9860-51edc88c5d8b"), "DKK", "kr", "🇩🇰", "Danish Krone" },
                    { new Guid("062cc5d5-534d-4728-a335-5a8fa7b20253"), "ZAR", "R", "🇿🇦", "South African Rand" },
                    { new Guid("15316d30-39c8-413a-9732-135280f5de4e"), "HKD", "$", "🇭🇰", "Hong Kong Dollar" },
                    { new Guid("29363e9f-6b1b-4ce0-9de3-a85b6320b7d3"), "CHF", "CHF", "🇨🇭", "Swiss Franc" },
                    { new Guid("30794d32-5a31-4009-94cb-af2c3f37b12a"), "MYR", "RM", "🇲🇾", "Malaysian Ringgit" },
                    { new Guid("33ff4d60-238b-40e7-9365-e58b89e03a2f"), "AUD", "$", "🇦🇺", "Australian Dollar" },
                    { new Guid("416a4510-398b-487d-af48-8b60ddef7fd4"), "SEK", "kr", "🇸🇪", "Swedish Krona" },
                    { new Guid("4a80793c-eedf-4c79-98c7-bc9c69941759"), "KRW", "₩", "🇰🇷", "South Korean Won" },
                    { new Guid("4b29d3d7-f0c6-4e06-8d93-266a624a48a5"), "AED", "د.إ", "🇦🇪", "United Arab Emirates Dirham" },
                    { new Guid("5a73ddf2-ea75-4605-bc35-f129a8f7d193"), "CNY", "¥", "🇨🇳", "Chinese Yuan" },
                    { new Guid("74589a3c-7482-4f56-bf4a-906316eab22b"), "JPY", "¥", "🇯🇵", "Japanese Yen" },
                    { new Guid("76941acb-779b-40c4-9503-d28bf8da705f"), "PHP", "₱", "🇵🇭", "Philippine Peso" },
                    { new Guid("82462286-5e89-4007-9bf8-1926f9bf9f36"), "PLN", "zł", "🇵🇱", "Polish Zloty" },
                    { new Guid("96e4b379-9964-4100-8178-4398098597c7"), "INR", "₹", "🇮🇳", "Indian Rupee" },
                    { new Guid("a6b9ebd8-4098-4891-917f-5297a134656d"), "SGD", "$", "🇸🇬", "Singapore Dollar" },
                    { new Guid("a92e6352-eff8-43c3-9683-6e1163aec392"), "EUR", "€", "🇪🇺", "Euro" },
                    { new Guid("acd0d4b2-9719-4b42-ad6f-fe5eefdb4ad6"), "NZD", "$", "🇳🇿", "New Zealand Dollar" },
                    { new Guid("b5b6bacb-4de2-44aa-b102-4bee612ce114"), "IDR", "Rp", "🇮🇩", "Indonesian Rupiah" },
                    { new Guid("c91ce637-1b8b-4d9e-aaef-009a293db2c0"), "TRY", "₺", "🇹🇷", "Turkish Lira" },
                    { new Guid("c9578262-a5e9-4381-9027-24ab665817fe"), "HUF", "Ft", "🇭🇺", "Hungarian Forint" },
                    { new Guid("cf7d8927-7850-4f6a-a948-805dbfe97d46"), "CAD", "$", "🇨🇦", "Canadian Dollar" },
                    { new Guid("d5f2f6fd-6db4-4028-8ead-5bade11d425e"), "THB", "฿", "🇹🇭", "Thai Baht" },
                    { new Guid("d60f057a-f38b-4d86-a863-59c21f1afc2a"), "BRL", "R$", "🇧🇷", "Brazilian Real" },
                    { new Guid("edc7c5e3-fb5f-4951-a3ef-18f8d19f8a36"), "GBP", "£", "🇬🇧", "British Pound Sterling" },
                    { new Guid("f594c21c-006a-45ef-be01-81af4d66c514"), "MXN", "$", "🇲🇽", "Mexican Peso" },
                    { new Guid("f5c9ecf8-21f0-4034-b086-a3a0bf1d0c31"), "USD", "$", "🇺🇸", "United States Dollar" },
                    { new Guid("ff1b7827-b99a-4234-a5c8-f13d1d23235d"), "NOK", "kr", "🇳🇴", "Norwegian Krone" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("01805c1a-e33b-463d-a02c-28896bbe52d0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("050c49ba-4ba4-422b-9860-51edc88c5d8b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("062cc5d5-534d-4728-a335-5a8fa7b20253"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("15316d30-39c8-413a-9732-135280f5de4e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("29363e9f-6b1b-4ce0-9de3-a85b6320b7d3"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("30794d32-5a31-4009-94cb-af2c3f37b12a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("33ff4d60-238b-40e7-9365-e58b89e03a2f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("416a4510-398b-487d-af48-8b60ddef7fd4"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4a80793c-eedf-4c79-98c7-bc9c69941759"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4b29d3d7-f0c6-4e06-8d93-266a624a48a5"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("5a73ddf2-ea75-4605-bc35-f129a8f7d193"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("74589a3c-7482-4f56-bf4a-906316eab22b"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("76941acb-779b-40c4-9503-d28bf8da705f"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("82462286-5e89-4007-9bf8-1926f9bf9f36"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("96e4b379-9964-4100-8178-4398098597c7"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a6b9ebd8-4098-4891-917f-5297a134656d"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("a92e6352-eff8-43c3-9683-6e1163aec392"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("acd0d4b2-9719-4b42-ad6f-fe5eefdb4ad6"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b5b6bacb-4de2-44aa-b102-4bee612ce114"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c91ce637-1b8b-4d9e-aaef-009a293db2c0"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c9578262-a5e9-4381-9027-24ab665817fe"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("cf7d8927-7850-4f6a-a948-805dbfe97d46"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d5f2f6fd-6db4-4028-8ead-5bade11d425e"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("d60f057a-f38b-4d86-a863-59c21f1afc2a"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("edc7c5e3-fb5f-4951-a3ef-18f8d19f8a36"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f594c21c-006a-45ef-be01-81af4d66c514"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("f5c9ecf8-21f0-4034-b086-a3a0bf1d0c31"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ff1b7827-b99a-4234-a5c8-f13d1d23235d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
