using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class IhavenoIdeawhatIamdoinganymore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId",
                table: "RandomGroupModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel");

            migrationBuilder.DropIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId",
                table: "RandomGroupModel");

            migrationBuilder.DropIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel");

            migrationBuilder.DropColumn(
                name: "QuestionaireModelQuestionaireId",
                table: "RandomGroupModel");

            migrationBuilder.DropColumn(
                name: "QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionaireModelQuestionaireId",
                table: "RandomGroupModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId",
                principalTable: "QuestionaireModels",
                principalColumn: "QuestionaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId1",
                principalTable: "QuestionaireModels",
                principalColumn: "QuestionaireId");
        }
    }
}
