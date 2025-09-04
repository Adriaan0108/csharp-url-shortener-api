using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_url_shortener_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfitSharingRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClickAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SharePercentage = table.Column<double>(type: "REAL", nullable: false),
                    ShareIncreasePercentage = table.Column<double>(type: "REAL", nullable: false),
                    IncreaseEveryNClicks = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxShareIncreasePercentage = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitSharingRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginalUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ShortUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urls_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlClicks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UrlId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfitSharingRuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountEarned = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlClicks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlClicks_ProfitSharingRules_ProfitSharingRuleId",
                        column: x => x.ProfitSharingRuleId,
                        principalTable: "ProfitSharingRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UrlClicks_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrlClicks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlClicks_ProfitSharingRuleId",
                table: "UrlClicks",
                column: "ProfitSharingRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlClicks_UrlId",
                table: "UrlClicks",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlClicks_UserId",
                table: "UrlClicks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_CreatedBy",
                table: "Urls",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ShortUrl",
                table: "Urls",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlClicks");

            migrationBuilder.DropTable(
                name: "ProfitSharingRules");

            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
