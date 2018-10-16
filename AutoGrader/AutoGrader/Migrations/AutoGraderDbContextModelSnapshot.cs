﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AutoGrader.DataAccess;
using AutoGrader.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoGrader.Migrations
{
    [DbContext(typeof(AutoGraderDbContext))]
    partial class AutoGraderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AutoGrader.Models.Assignment.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClassId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<List<Language>>("Languages");

                    b.Property<int>("MemoryLimit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TimeLimit");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("AutoGrader.Models.Assignment.TestCase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignmentId");

                    b.Property<string>("ExpectedOutput");

                    b.Property<string>("Feedback");

                    b.Property<string>("Input");

                    b.HasKey("ID");

                    b.HasIndex("AssignmentId");

                    b.ToTable("TestCase");
                });

            modelBuilder.Entity("AutoGrader.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("InstructorId");

                    b.Property<string>("Name");

                    b.Property<int?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.Submission", b =>
                {
                    b.Property<int>("SubmissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentId");

                    b.Property<int?>("InputId");

                    b.Property<int?>("OutputId");

                    b.Property<DateTime>("SubmissionTime");

                    b.Property<int>("UserId");

                    b.HasKey("SubmissionId");

                    b.HasIndex("InputId");

                    b.HasIndex("OutputId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.SubmissionInput", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignmentId");

                    b.Property<int>("Language");

                    b.Property<string>("SourceCode");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("SubmissionInputs");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.SubmissionOutput", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompileOutput");

                    b.Property<bool>("Compiled");

                    b.Property<double>("Runtime");

                    b.HasKey("Id");

                    b.ToTable("SubmissionOutputs");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.TestCaseReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodeInput");

                    b.Property<string>("CodeOutput");

                    b.Property<string>("ExpectedOutput");

                    b.Property<string>("Feedback");

                    b.Property<bool>("Pass");

                    b.Property<int>("Runtime");

                    b.Property<int?>("SubmissionOutputId");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionOutputId");

                    b.ToTable("TestCaseReports");
                });

            modelBuilder.Entity("AutoGrader.Models.Users.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsInstructor");

                    b.Property<bool>("IsStudent");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("AutoGrader.Models.Users.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsInstructor");

                    b.Property<bool>("IsStudent");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AutoGrader.Models.Assignment.Assignment", b =>
                {
                    b.HasOne("AutoGrader.Models.Class")
                        .WithMany("Assignments")
                        .HasForeignKey("ClassId");
                });

            modelBuilder.Entity("AutoGrader.Models.Assignment.TestCase", b =>
                {
                    b.HasOne("AutoGrader.Models.Assignment.Assignment")
                        .WithMany("TestCases")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("AutoGrader.Models.Class", b =>
                {
                    b.HasOne("AutoGrader.Models.Users.Instructor", "Instructor")
                        .WithMany("Classes")
                        .HasForeignKey("InstructorId");

                    b.HasOne("AutoGrader.Models.Users.Student")
                        .WithMany("Classes")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.Submission", b =>
                {
                    b.HasOne("AutoGrader.Models.Submission.SubmissionInput", "Input")
                        .WithMany()
                        .HasForeignKey("InputId");

                    b.HasOne("AutoGrader.Models.Submission.SubmissionOutput", "Output")
                        .WithMany()
                        .HasForeignKey("OutputId");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.SubmissionInput", b =>
                {
                    b.HasOne("AutoGrader.Models.Assignment.Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("AutoGrader.Models.Submission.TestCaseReport", b =>
                {
                    b.HasOne("AutoGrader.Models.Submission.SubmissionOutput")
                        .WithMany("TestCases")
                        .HasForeignKey("SubmissionOutputId");
                });
#pragma warning restore 612, 618
        }
    }
}
