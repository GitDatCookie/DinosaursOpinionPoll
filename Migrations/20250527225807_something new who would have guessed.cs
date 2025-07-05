using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class somethingnewwhowouldhaveguessed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroupModel_RandomGroupId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionairePages_RandomGroupModel_RandomGroupId",
                table: "QuestionairePages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RandomGroupModel",
                table: "RandomGroupModel");

            migrationBuilder.RenameTable(
                name: "RandomGroupModel",
                newName: "RandomGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RandomGroups",
                table: "RandomGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroups_RandomGroupId",
                table: "QuestionaireComponentModelBase",
                column: "RandomGroupId",
                principalTable: "RandomGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionairePages_RandomGroups_RandomGroupId",
                table: "QuestionairePages",
                column: "RandomGroupId",
                principalTable: "RandomGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroups_RandomGroupId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionairePages_RandomGroups_RandomGroupId",
                table: "QuestionairePages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RandomGroups",
                table: "RandomGroups");

            migrationBuilder.RenameTable(
                name: "RandomGroups",
                newName: "RandomGroupModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RandomGroupModel",
                table: "RandomGroupModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroupModel_RandomGroupId",
                table: "QuestionaireComponentModelBase",
                column: "RandomGroupId",
                principalTable: "RandomGroupModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionairePages_RandomGroupModel_RandomGroupId",
                table: "QuestionairePages",
                column: "RandomGroupId",
                principalTable: "RandomGroupModel",
                principalColumn: "Id");
        }
    }
}
