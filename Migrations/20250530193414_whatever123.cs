using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class whatever123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ControlGroup",
                table: "SubjectUsers",
                newName: "IsTreatmentGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupType",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupType",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.RenameColumn(
                name: "IsTreatmentGroup",
                table: "SubjectUsers",
                newName: "ControlGroup");
        }
    }
}
