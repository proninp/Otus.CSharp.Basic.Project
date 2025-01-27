using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CurrencyCode = table.Column<string>(type: "text", nullable: false),
                    CurrencySign = table.Column<string>(type: "text", nullable: false),
                    Emoji = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FromAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    FromAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ToAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_ToAccountId",
                        column: x => x.ToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromAccountId",
                table: "Transfers",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToAccountId",
                table: "Transfers",
                column: "ToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
