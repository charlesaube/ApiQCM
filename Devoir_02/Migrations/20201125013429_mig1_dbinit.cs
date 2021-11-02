using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Devoir_02.Migrations
{
    public partial class mig1_dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    IsRight = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionQuizzes",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false),
                    QuizID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuizzes", x => new { x.QuestionID, x.QuizID });
                    table.ForeignKey(
                        name: "FK_QuestionQuizzes_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuizzes_Quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OptionID = table.Column<int>(nullable: false),
                    QuizID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_Answers_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Quizzes_QuizID",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Description" },
                values: new object[,]
                {
                    { 1, "easy" },
                    { 2, "medium" },
                    { 3, "hard" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                column: "QuizID",
                values: new object[]
                {
                    1,
                    2
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "CategoryID", "Text", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "Java is ...", "multiplechoice", 1 },
                    { 5, 1, "Local variables are declared in methods, constructors, or blocks.", "multiplechoice", 1 },
                    { 2, 2, "A Java class", "checkboxes", 1 },
                    { 3, 2, "What is Java inheritance?", "multiplechoice", 1 },
                    { 6, 2, "... stores a fixed-size sequential collection of elements of the same type?", "multiplechoice", 1 },
                    { 4, 3, "Polymorphism is the ability of an object to take on many forms.", "multiplechoice", 1 }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "OptionID", "IsRight", "QuestionID", "Text" },
                values: new object[,]
                {
                    { 1, 0, 1, "a coffee" },
                    { 10, 0, 4, "false" },
                    { 9, 1, 4, "true" },
                    { 15, 0, 6, "methods" },
                    { 14, 1, 6, "arrays" },
                    { 13, 0, 6, "variables" },
                    { 8, 0, 3, "it mainly used to traverse collection of elements including arrays." },
                    { 7, 0, 3, "a problem that arises during the execution of a program." },
                    { 5, 1, 2, "can have any number of methods" },
                    { 6, 1, 3, "the process where one class acquires the properties (methods and fields) of another." },
                    { 12, 0, 5, "false" },
                    { 11, 1, 5, "true" },
                    { 3, 0, 1, "a source code editor" },
                    { 2, 1, 1, "a high-level programming language" },
                    { 4, 1, 2, "is a template that describes the behavior that the object of its type support" }
                });

            migrationBuilder.InsertData(
                table: "QuestionQuizzes",
                columns: new[] { "QuestionID", "QuizID" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 4, 1 },
                    { 5, 2 },
                    { 3, 2 },
                    { 1, 2 },
                    { 1, 1 },
                    { 6, 1 },
                    { 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerID", "OptionID", "QuizID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 6, 1, 2 },
                    { 9, 11, 2 },
                    { 2, 4, 1 },
                    { 3, 5, 1 },
                    { 7, 7, 2 },
                    { 5, 14, 1 },
                    { 4, 9, 1 },
                    { 8, 9, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_OptionID",
                table: "Answers",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuizID",
                table: "Answers",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuizzes_QuizID",
                table: "QuestionQuizzes",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryID",
                table: "Questions",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionQuizzes");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
