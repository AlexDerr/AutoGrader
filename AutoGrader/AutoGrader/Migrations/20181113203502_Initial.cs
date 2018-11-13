using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoGrader.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    NormalizedUserName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    NormalizedUserName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Compiled = table.Column<bool>(nullable: false),
                    Runtime = table.Column<double>(nullable: false),
                    CompileOutput = table.Column<string>(nullable: true),
                    MemoryLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ClassKey = table.Column<string>(nullable: true),
                    InstructorId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Pass = table.Column<bool>(nullable: false),
                    CodeInput = table.Column<string>(nullable: true),
                    CodeOutput = table.Column<string>(nullable: true),
                    ExpectedOutput = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Runtime = table.Column<int>(nullable: false),
                    SubmissionOutputId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCaseReports_SubmissionOutputs_SubmissionOutputId",
                        column: x => x.SubmissionOutputId,
                        principalTable: "SubmissionOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MemoryLimit = table.Column<int>(nullable: false),
                    TimeLimit = table.Column<int>(nullable: false),
                    Languages = table.Column<List<string>>(nullable: true),
                    ClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionInputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SourceCode = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    AssignmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionInputs_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Input = table.Column<string>(nullable: true),
                    ExpectedOutput = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    AssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TestCaseSpecifications_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    AssignmentId = table.Column<int>(nullable: false),
                    SubmissionTime = table.Column<DateTime>(nullable: false),
                    InputId = table.Column<int>(nullable: true),
                    OutputId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_SubmissionInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "SubmissionInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissions_SubmissionOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "SubmissionOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ClassId",
                table: "Assignments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorId",
                table: "Classes",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentId",
                table: "Classes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionInputs_AssignmentId",
                table: "SubmissionInputs",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_InputId",
                table: "Submissions",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_OutputId",
                table: "Submissions",
                column: "OutputId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseReports_SubmissionOutputId",
                table: "TestCaseReports",
                column: "SubmissionOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseSpecifications_AssignmentId",
                table: "TestCaseSpecifications",
                column: "AssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "TestCaseReports");

            migrationBuilder.DropTable(
                name: "TestCaseSpecifications");

            migrationBuilder.DropTable(
                name: "SubmissionInputs");

            migrationBuilder.DropTable(
                name: "SubmissionOutputs");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
