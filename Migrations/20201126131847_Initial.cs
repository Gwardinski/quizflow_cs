using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizFlow.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    points = table.Column<double>(type: "float", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    questionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPublished = table.Column<bool>(type: "bit", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalPoints = table.Column<double>(type: "float", nullable: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalPoints = table.Column<double>(type: "float", nullable: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rounds_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizRounds",
                columns: table => new
                {
                    quizId = table.Column<int>(type: "int", nullable: false),
                    roundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizRounds", x => new { x.quizId, x.roundId });
                    table.ForeignKey(
                        name: "FK_QuizRounds_Quizzes_quizId",
                        column: x => x.quizId,
                        principalTable: "Quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizRounds_Rounds_roundId",
                        column: x => x.roundId,
                        principalTable: "Rounds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundQuestions",
                columns: table => new
                {
                    roundId = table.Column<int>(type: "int", nullable: false),
                    questionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundQuestions", x => new { x.roundId, x.questionId });
                    table.ForeignKey(
                        name: "FK_RoundQuestions_Questions_questionId",
                        column: x => x.questionId,
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundQuestions_Rounds_roundId",
                        column: x => x.roundId,
                        principalTable: "Rounds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_userid",
                table: "Questions",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_QuizRounds_roundId",
                table: "QuizRounds",
                column: "roundId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_userid",
                table: "Quizzes",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_RoundQuestions_questionId",
                table: "RoundQuestions",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_userid",
                table: "Rounds",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizRounds");

            migrationBuilder.DropTable(
                name: "RoundQuestions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
