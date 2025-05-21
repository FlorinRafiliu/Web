using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProgramming.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_studentSid",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_studentSid",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "studentSid",
                table: "Grades");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Sid",
                table: "Grades",
                column: "Sid");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_Sid",
                table: "Grades",
                column: "Sid",
                principalTable: "Students",
                principalColumn: "Sid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_Sid",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_Sid",
                table: "Grades");

            migrationBuilder.AddColumn<int>(
                name: "studentSid",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_studentSid",
                table: "Grades",
                column: "studentSid");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_studentSid",
                table: "Grades",
                column: "studentSid",
                principalTable: "Students",
                principalColumn: "Sid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
