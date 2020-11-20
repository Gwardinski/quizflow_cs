using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizFlow.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Rounds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Rounds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Rounds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdated",
                table: "Rounds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "totalPoints",
                table: "Rounds",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "difficulty",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdated",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "points",
                table: "Questions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "questionType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "lastUpdated",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "totalPoints",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "category",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "lastUpdated",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "points",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "questionType",
                table: "Questions");
        }
    }
}
