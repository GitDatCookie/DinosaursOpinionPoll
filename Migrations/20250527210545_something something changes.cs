using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class somethingsomethingchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroup_RandomGroupId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionairePages_RandomGroup_RandomGroupId",
                table: "QuestionairePages");

            migrationBuilder.DropTable(
                name: "RandomGroup");

            migrationBuilder.AddColumn<bool>(
                name: "IsRandomised",
                table: "QuestionaireComponentModelBase",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RandomGroupModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomGroupType = table.Column<int>(type: "int", nullable: false),
                    QuestionaireModelQuestionaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionaireModelQuestionaireId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomGroupModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId",
                        column: x => x.QuestionaireModelQuestionaireId,
                        principalTable: "QuestionaireModels",
                        principalColumn: "QuestionaireId");
                    table.ForeignKey(
                        name: "FK_RandomGroupModel_QuestionaireModels_QuestionaireModelQuestionaireId1",
                        column: x => x.QuestionaireModelQuestionaireId1,
                        principalTable: "QuestionaireModels",
                        principalColumn: "QuestionaireId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroupModel_QuestionaireModelQuestionaireId1",
                table: "RandomGroupModel",
                column: "QuestionaireModelQuestionaireId1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroupModel_RandomGroupId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionairePages_RandomGroupModel_RandomGroupId",
                table: "QuestionairePages");

            migrationBuilder.DropTable(
                name: "RandomGroupModel");

            migrationBuilder.DropColumn(
                name: "IsRandomised",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.CreateTable(
                name: "RandomGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionaireModelQuestionaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionaireModelQuestionaireId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RandomGroupType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandomGroup_QuestionaireModels_QuestionaireModelQuestionaireId",
                        column: x => x.QuestionaireModelQuestionaireId,
                        principalTable: "QuestionaireModels",
                        principalColumn: "QuestionaireId");
                    table.ForeignKey(
                        name: "FK_RandomGroup_QuestionaireModels_QuestionaireModelQuestionaireId1",
                        column: x => x.QuestionaireModelQuestionaireId1,
                        principalTable: "QuestionaireModels",
                        principalColumn: "QuestionaireId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroup_QuestionaireModelQuestionaireId",
                table: "RandomGroup",
                column: "QuestionaireModelQuestionaireId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroup_QuestionaireModelQuestionaireId1",
                table: "RandomGroup",
                column: "QuestionaireModelQuestionaireId1");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_RandomGroup_RandomGroupId",
                table: "QuestionaireComponentModelBase",
                column: "RandomGroupId",
                principalTable: "RandomGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionairePages_RandomGroup_RandomGroupId",
                table: "QuestionairePages",
                column: "RandomGroupId",
                principalTable: "RandomGroup",
                principalColumn: "Id");
        }
    }
}
