using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizFlow.Migrations {
  public partial class UserQuestionRelation : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AddColumn<int>(
          name: "userId",
          table: "Questions",
          type: "int",
          nullable: true);

      migrationBuilder.CreateIndex(
          name: "IX_Questions_userId",
          table: "Questions",
          column: "userId");

      migrationBuilder.AddForeignKey(
          name: "FK_Questions_Users_userId",
          table: "Questions",
          column: "userId",
          principalTable: "Users",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropForeignKey(
          name: "FK_Questions_Users_userId",
          table: "Questions");

      migrationBuilder.DropIndex(
          name: "IX_Questions_userId",
          table: "Questions");

      migrationBuilder.DropColumn(
          name: "userId",
          table: "Questions");
    }
  }
}
