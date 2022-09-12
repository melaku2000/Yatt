using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddAdminAndPaymentModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NoOfCandidateInterview",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfJobPost",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "ServicePeriodInMonth",
                table: "Subscriptions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegisterarId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admins_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServicePeriodInMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    NoOfJobPost = table.Column<int>(type: "int", nullable: false),
                    NoOfCandidateInterview = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 9, 12, 20, 43, 41, 949, DateTimeKind.Utc).AddTicks(5319), new DateTime(2022, 9, 12, 20, 43, 41, 949, DateTimeKind.Utc).AddTicks(5319) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 12, 20, 43, 41, 937, DateTimeKind.Utc).AddTicks(1830), new DateTime(2022, 9, 12, 20, 43, 41, 937, DateTimeKind.Utc).AddTicks(1830), new byte[] { 1, 200, 187, 52, 165, 133, 117, 18, 54, 167, 184, 74, 38, 47, 65, 72, 118, 14, 173, 66, 87, 116, 48, 217 }, new byte[] { 32, 167, 46, 195, 255, 229, 94, 3, 87, 142, 2, 6, 153, 64, 225, 193, 204, 163, 38, 152, 241, 8, 221, 143 } });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_CountryId",
                table: "Admins",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AdminId",
                table: "Payments",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NoOfCandidateInterview",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NoOfJobPost",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ServicePeriodInMonth",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 9, 10, 0, 38, 32, 886, DateTimeKind.Utc).AddTicks(5160), new DateTime(2022, 9, 10, 0, 38, 32, 886, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 10, 0, 38, 32, 873, DateTimeKind.Utc).AddTicks(6025), new DateTime(2022, 9, 10, 0, 38, 32, 873, DateTimeKind.Utc).AddTicks(6025), new byte[] { 54, 124, 129, 70, 168, 143, 183, 179, 211, 79, 208, 47, 255, 100, 156, 161, 117, 36, 129, 192, 187, 99, 246, 110 }, new byte[] { 236, 143, 166, 255, 252, 3, 219, 55, 48, 225, 218, 70, 216, 200, 219, 125, 5, 76, 156, 210, 145, 17, 241, 214 } });
        }
    }
}
