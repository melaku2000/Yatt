using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yatt.Api.Migrations
{
    public partial class AddingJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "NoOfCandidateInterview",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "NoOfJobPost",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ServicePeriodInMonth",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscrioptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DeadLineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplyUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VacancyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplayUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplayLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobDescriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descripttion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDescriptions_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobDuties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Duty = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDuties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDuties_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobEducations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearsOfExperiance = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobEducations_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobQualifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobQualifications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_JobDescriptions_JobId",
                table: "JobDescriptions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDuties_JobId",
                table: "JobDuties",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobEducations_JobId",
                table: "JobEducations",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobQualifications_JobId",
                table: "JobQualifications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_VacancyId",
                table: "Jobs",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_SubscrioptionId",
                table: "Vacancies",
                column: "SubscrioptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobDescriptions");

            migrationBuilder.DropTable(
                name: "JobDuties");

            migrationBuilder.DropTable(
                name: "JobEducations");

            migrationBuilder.DropTable(
                name: "JobQualifications");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfCandidateInterview",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfJobPost",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "ServicePeriodInMonth",
                table: "Payments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
