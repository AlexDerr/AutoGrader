﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AutoGrader.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoGrader.Migrations
{
    [DbContext(typeof(AutoGraderDbContext))]
    [Migration("20180918181322_AddAssigmentTable")]
    partial class AddAssigmentTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<List<string>>("Feedbacks");

                    b.Property<List<string>>("InputFileNames");

                    b.Property<List<string>>("Languages");

                    b.Property<int>("MemoryLimit");

                    b.Property<string>("Name");

                    b.Property<List<string>>("OutputFileNames");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TimeLimit");

                    b.HasKey("Id");

                    b.ToTable("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}