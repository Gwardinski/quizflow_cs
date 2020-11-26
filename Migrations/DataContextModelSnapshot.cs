﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizFlow.Data;

namespace QuizFlow.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("difficulty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("lastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<double>("points")
                        .HasColumnType("float");

                    b.Property<string>("question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("questionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizFlow.Models.Quiz", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("lastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("totalPoints")
                        .HasColumnType("float");

                    b.Property<int?>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("QuizFlow.Models.QuizRound", b =>
                {
                    b.Property<int>("quizId")
                        .HasColumnType("int");

                    b.Property<int>("roundId")
                        .HasColumnType("int");

                    b.HasKey("quizId", "roundId");

                    b.HasIndex("roundId");

                    b.ToTable("QuizRounds");
                });

            modelBuilder.Entity("QuizFlow.Models.Round", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPublished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("lastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("totalPoints")
                        .HasColumnType("float");

                    b.Property<int?>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userid");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("QuizFlow.Models.RoundQuestion", b =>
                {
                    b.Property<int>("roundId")
                        .HasColumnType("int");

                    b.Property<int>("questionId")
                        .HasColumnType("int");

                    b.HasKey("roundId", "questionId");

                    b.HasIndex("questionId");

                    b.ToTable("RoundQuestions");
                });

            modelBuilder.Entity("QuizFlow.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuizFlow.Models.Question", b =>
                {
                    b.HasOne("QuizFlow.Models.User", "user")
                        .WithMany("questions")
                        .HasForeignKey("userid");

                    b.Navigation("user");
                });

            modelBuilder.Entity("QuizFlow.Models.Quiz", b =>
                {
                    b.HasOne("QuizFlow.Models.User", "user")
                        .WithMany("quizzes")
                        .HasForeignKey("userid");

                    b.Navigation("user");
                });

            modelBuilder.Entity("QuizFlow.Models.QuizRound", b =>
                {
                    b.HasOne("QuizFlow.Models.Quiz", "quiz")
                        .WithMany("quizRounds")
                        .HasForeignKey("quizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuizFlow.Models.Round", "round")
                        .WithMany("quizRounds")
                        .HasForeignKey("roundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("quiz");

                    b.Navigation("round");
                });

            modelBuilder.Entity("QuizFlow.Models.Round", b =>
                {
                    b.HasOne("QuizFlow.Models.User", "user")
                        .WithMany("rounds")
                        .HasForeignKey("userid");

                    b.Navigation("user");
                });

            modelBuilder.Entity("QuizFlow.Models.RoundQuestion", b =>
                {
                    b.HasOne("QuizFlow.Models.Question", "question")
                        .WithMany("roundQuestions")
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuizFlow.Models.Round", "round")
                        .WithMany("roundQuestions")
                        .HasForeignKey("roundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");

                    b.Navigation("round");
                });

            modelBuilder.Entity("QuizFlow.Models.Question", b =>
                {
                    b.Navigation("roundQuestions");
                });

            modelBuilder.Entity("QuizFlow.Models.Quiz", b =>
                {
                    b.Navigation("quizRounds");
                });

            modelBuilder.Entity("QuizFlow.Models.Round", b =>
                {
                    b.Navigation("quizRounds");

                    b.Navigation("roundQuestions");
                });

            modelBuilder.Entity("QuizFlow.Models.User", b =>
                {
                    b.Navigation("questions");

                    b.Navigation("quizzes");

                    b.Navigation("rounds");
                });
#pragma warning restore 612, 618
        }
    }
}
