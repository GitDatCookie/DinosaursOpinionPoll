using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class WhaETVER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionTextOption",
                table: "QuestionaireComponentModelBase",
                newName: "TitleFieldType");

            migrationBuilder.AddColumn<int>(
                name: "QuestionAnswerFieldType",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionAnswerFieldType",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.RenameColumn(
                name: "TitleFieldType",
                table: "QuestionaireComponentModelBase",
                newName: "QuestionTextOption");
        }
    }
}
