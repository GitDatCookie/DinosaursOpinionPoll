using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class Stylingshit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentColour",
                table: "QuestionaireComponentModelBase");

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
                name: "QuestionAnswerFieldType",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "TextVariant",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentStyleId",
                table: "QuestionaireComponentModelBase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComponentStyleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentColour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLabelColourised = table.Column<bool>(type: "bit", nullable: false),
                    HelperText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextVariant = table.Column<int>(type: "int", nullable: false),
                    QuestionAnswerFieldType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentStyleModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaireComponentModelBase_ComponentStyleId",
                table: "QuestionaireComponentModelBase",
                column: "ComponentStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_ComponentStyleModel_ComponentStyleId",
                table: "QuestionaireComponentModelBase",
                column: "ComponentStyleId",
                principalTable: "ComponentStyleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_ComponentStyleModel_ComponentStyleId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropTable(
                name: "ComponentStyleModel");

            migrationBuilder.DropIndex(
                name: "IX_QuestionaireComponentModelBase_ComponentStyleId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropColumn(
                name: "ComponentStyleId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.AddColumn<string>(
                name: "ComponentColour",
                table: "QuestionaireComponentModelBase",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "QuestionAnswerFieldType",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextVariant",
                table: "QuestionaireComponentModelBase",
                type: "int",
                nullable: true);
        }
    }
}
