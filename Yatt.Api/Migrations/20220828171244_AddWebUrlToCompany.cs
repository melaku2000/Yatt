using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddWebUrlToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PhoneConfirmed",
                table: "CompanyDetails");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CompanyDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebUrl",
                table: "CompanyDetails",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 8, 28, 17, 12, 43, 553, DateTimeKind.Utc).AddTicks(3901), new DateTime(2022, 8, 28, 17, 12, 43, 553, DateTimeKind.Utc).AddTicks(3901) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 8, 28, 17, 12, 43, 538, DateTimeKind.Utc).AddTicks(8542), new DateTime(2022, 8, 28, 17, 12, 43, 538, DateTimeKind.Utc).AddTicks(8542), new byte[] { 119, 151, 95, 15, 172, 241, 226, 106, 78, 105, 178, 71, 148, 112, 247, 240, 21, 100, 139, 179, 0, 5, 31, 33 }, new byte[] { 155, 57, 194, 95, 202, 149, 44, 25, 244, 17, 254, 44, 237, 179, 19, 22, 7, 228, 13, 249, 101, 79, 170, 68 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CompanyDetails");

            migrationBuilder.DropColumn(
                name: "WebUrl",
                table: "CompanyDetails");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneConfirmed",
                table: "CompanyDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 8, 27, 20, 43, 53, 309, DateTimeKind.Utc).AddTicks(6524), new DateTime(2022, 8, 27, 20, 43, 53, 309, DateTimeKind.Utc).AddTicks(6524) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 8, 27, 20, 43, 53, 297, DateTimeKind.Utc).AddTicks(998), new DateTime(2022, 8, 27, 20, 43, 53, 297, DateTimeKind.Utc).AddTicks(998), new byte[] { 223, 182, 234, 46, 229, 9, 151, 91, 24, 133, 252, 49, 66, 41, 33, 223, 26, 122, 149, 85, 142, 161, 72, 189 }, new byte[] { 11, 131, 84, 71, 146, 145, 150, 104, 187, 11, 92, 110, 198, 241, 154, 12, 94, 56, 48, 144, 34, 232, 152, 34 } });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Countries_CountryId",
                table: "Companies",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
