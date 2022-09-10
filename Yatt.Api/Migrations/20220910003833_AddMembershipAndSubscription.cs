using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddMembershipAndSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServicePeriodInMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    NoOfJobPost = table.Column<int>(type: "int", nullable: false),
                    NoOfCandidateInterview = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MembershipId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CompanyId",
                table: "Subscriptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MembershipId",
                table: "Subscriptions",
                column: "MembershipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 59, 38, 95, DateTimeKind.Utc).AddTicks(4454), new DateTime(2022, 9, 9, 21, 59, 38, 95, DateTimeKind.Utc).AddTicks(4454) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 59, 38, 83, DateTimeKind.Utc).AddTicks(3935), new DateTime(2022, 9, 9, 21, 59, 38, 83, DateTimeKind.Utc).AddTicks(3935), new byte[] { 62, 79, 10, 74, 118, 212, 244, 36, 18, 198, 3, 32, 218, 33, 5, 194, 183, 155, 223, 176, 172, 207, 209, 92 }, new byte[] { 1, 137, 46, 146, 117, 221, 68, 191, 14, 17, 62, 216, 251, 60, 120, 52, 53, 224, 28, 126, 203, 165, 139, 190 } });
        }
    }
}
