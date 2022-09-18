using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddJobApplicationApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplitionDate",
                table: "Educations",
                newName: "ComplitionYear");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
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
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId",
                unique: true,
                filter: "[CandidateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ComplitionYear",
                table: "Educations",
                newName: "ComplitionDate");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "UserId",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate" },
                values: new object[] { new DateTime(2022, 9, 12, 22, 0, 10, 952, DateTimeKind.Utc).AddTicks(5467), new DateTime(2022, 9, 12, 22, 0, 10, 952, DateTimeKind.Utc).AddTicks(5467) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "melaku1234",
                columns: new[] { "CreatedDate", "ModifyDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2022, 9, 12, 22, 0, 10, 940, DateTimeKind.Utc).AddTicks(8943), new DateTime(2022, 9, 12, 22, 0, 10, 940, DateTimeKind.Utc).AddTicks(8943), new byte[] { 161, 44, 9, 248, 23, 134, 20, 156, 98, 241, 3, 142, 42, 167, 38, 5, 163, 87, 47, 102, 221, 19, 201, 213 }, new byte[] { 51, 105, 129, 4, 32, 233, 143, 13, 36, 127, 237, 42, 84, 209, 244, 37, 27, 245, 27, 173, 72, 238, 134, 93 } });
        }
    }
}
