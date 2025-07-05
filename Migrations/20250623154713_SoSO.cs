using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class SoSO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLabelColourised",
                table: "QuestionaireComponentModelBase",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlaceholderColourised",
                table: "QuestionaireComponentModelBase",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "QuestionaireComponentModelBase",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placeholder",
                table: "QuestionaireComponentModelBase",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionTextOption",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TextVariant",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLabelColourised",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "IsPlaceholderColourised",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "Placeholder",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "QuestionTextOption",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "TextVariant",
                table: "QuestionaireComponentModelBase");
        }
    }
}
