using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddingEducationAndExperiance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profession");

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AcademyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AcademyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComplitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experiances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiances_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Experiances_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CandidateId",
                table: "Educations",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiances_CandidateId",
                table: "Experiances",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiances_DomainId",
                table: "Experiances",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiances");

            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profession_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Profession_DomainId",
                table: "Profession",
                column: "DomainId");
        }
    }
}
