using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class removeVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Vacancies_VacancyId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "Jobs",
                newName: "SubscrioptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_VacancyId",
                table: "Jobs",
                newName: "IX_Jobs_SubscrioptionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLineDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 10, 12, 16, 34, 27, 363, DateTimeKind.Utc).AddTicks(5543), new DateTime(2022, 10, 12, 16, 34, 27, 363, DateTimeKind.Utc).AddTicks(5543) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 10, 12, 16, 34, 27, 336, DateTimeKind.Utc).AddTicks(9713), new DateTime(2022, 10, 12, 16, 34, 27, 336, DateTimeKind.Utc).AddTicks(9713), new byte[] { 60, 61, 231, 160, 252, 23, 215, 246, 217, 220, 59, 60, 9, 188, 97, 97, 132, 94, 250, 152, 210, 57, 40, 98 }, new byte[] { 120, 224, 198, 79, 153, 112, 124, 86, 112, 150, 220, 113, 187, 22, 44, 41, 64, 158, 179, 152, 145, 12, 49, 242 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Subscriptions_SubscrioptionId",
                table: "Jobs",
                column: "SubscrioptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Subscriptions_SubscrioptionId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DeadLineDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "SubscrioptionId",
                table: "Jobs",
                newName: "VacancyId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_SubscrioptionId",
                table: "Jobs",
                newName: "IX_Jobs_VacancyId");

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscrioptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApplyUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadLineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacancies_Subscriptions_SubscrioptionId",
                        column: x => x.SubscrioptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 2, 55, 924, DateTimeKind.Utc).AddTicks(5992), new DateTime(2022, 9, 18, 22, 2, 55, 924, DateTimeKind.Utc).AddTicks(5992) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 2, 55, 912, DateTimeKind.Utc).AddTicks(6116), new DateTime(2022, 9, 18, 22, 2, 55, 912, DateTimeKind.Utc).AddTicks(6116), new byte[] { 11, 233, 167, 78, 111, 48, 235, 6, 231, 11, 70, 84, 110, 99, 109, 90, 160, 226, 170, 98, 159, 197, 111, 16 }, new byte[] { 190, 174, 248, 147, 86, 18, 60, 118, 119, 56, 110, 182, 178, 31, 54, 184, 90, 53, 227, 14, 77, 87, 241, 180 } });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_SubscrioptionId",
                table: "Vacancies",
                column: "SubscrioptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Vacancies_VacancyId",
                table: "Jobs",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id");
        }
    }
}
