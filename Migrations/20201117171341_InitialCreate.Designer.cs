﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizFlow.Data;

namespace QuizFlow.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201117171341_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("QuizFlow.Models.Question", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
