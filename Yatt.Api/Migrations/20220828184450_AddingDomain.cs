using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddingDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DomainId",
                table: "CompanyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 8, 28, 18, 44, 49, 962, DateTimeKind.Utc).AddTicks(971), new DateTime(2022, 8, 28, 18, 44, 49, 962, DateTimeKind.Utc).AddTicks(971) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 8, 28, 18, 44, 49, 937, DateTimeKind.Utc).AddTicks(3833), new DateTime(2022, 8, 28, 18, 44, 49, 937, DateTimeKind.Utc).AddTicks(3833), new byte[] { 8, 244, 188, 157, 142, 198, 155, 183, 28, 236, 43, 219, 227, 73, 10, 93, 213, 137, 151, 163, 58, 67, 29, 119 }, new byte[] { 32, 79, 198, 109, 15, 16, 176, 156, 140, 115, 23, 154, 237, 231, 33, 86, 172, 166, 52, 152, 172, 243, 191, 165 } });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_DomainId",
                table: "CompanyDetails",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDetails_Domains_DomainId",
                table: "CompanyDetails",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDetails_Domains_DomainId",
                table: "CompanyDetails");

            migrationBuilder.DropIndex(
                name: "IX_CompanyDetails_DomainId",
                table: "CompanyDetails");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "CompanyDetails");

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
    }
}
