using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "SubjectUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ControlGroup = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionaireModels",
                columns: table => new
                {
                    QuestionaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionaireTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminUserModelLoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionaireModels", x => x.QuestionaireId);
                    table.ForeignKey(
                        name: "FK_QuestionaireModels_AdminUsers_AdminUserModelLoginId",
                        column: x => x.AdminUserModelLoginId,
                        principalTable: "AdminUsers",
                        principalColumn: "LoginId");
                });

            migrationBuilder.CreateTable(
                name: "RandomGroup",
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

            migrationBuilder.CreateTable(
                name: "QuestionairePages",
                columns: table => new
                {
                    PageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    IsRandomised = table.Column<bool>(type: "bit", nullable: false),
                    RandomGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionaireModelQuestionaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionairePages", x => x.PageID);
                    table.ForeignKey(
                        name: "FK_QuestionairePages_QuestionaireModels_QuestionaireModelQuestionaireId",
                        column: x => x.QuestionaireModelQuestionaireId,
                        principalTable: "QuestionaireModels",
                        principalColumn: "QuestionaireId");
                    table.ForeignKey(
                        name: "FK_QuestionairePages_RandomGroup_RandomGroupId",
                        column: x => x.RandomGroupId,
                        principalTable: "RandomGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionaireComponentModelBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    QuestionairePageModelPageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FreeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<int>(type: "int", nullable: true),
                    ComponentColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminUserModelLoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoModel_Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionaireComponentModelBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionaireComponentModelBase_AdminUsers_AdminUserModelLoginId",
                        column: x => x.AdminUserModelLoginId,
                        principalTable: "AdminUsers",
                        principalColumn: "LoginId");
                    table.ForeignKey(
                        name: "FK_QuestionaireComponentModelBase_QuestionairePages_QuestionairePageModelPageID",
                        column: x => x.QuestionairePageModelPageID,
                        principalTable: "QuestionairePages",
                        principalColumn: "PageID");
                    table.ForeignKey(
                        name: "FK_QuestionaireComponentModelBase_RandomGroup_RandomGroupId",
                        column: x => x.RandomGroupId,
                        principalTable: "RandomGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionaireComponentModelBase_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionaireComponentModelBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerModelSubjectUserModel",
                columns: table => new
                {
                    AnswersAnswerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerModelSubjectUserModel", x => new { x.AnswersAnswerID, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_AnswerModelSubjectUserModel_Answers_AnswersAnswerID",
                        column: x => x.AnswersAnswerID,
                        principalTable: "Answers",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerModelSubjectUserModel_SubjectUsers_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "SubjectUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerModelSubjectUserModel_UsersUserId",
                table: "AnswerModelSubjectUserModel",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaireComponentModelBase_AdminUserModelLoginId",
                table: "QuestionaireComponentModelBase",
                column: "AdminUserModelLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaireComponentModelBase_QuestionairePageModelPageID",
                table: "QuestionaireComponentModelBase",
                column: "QuestionairePageModelPageID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaireComponentModelBase_RandomGroupId",
                table: "QuestionaireComponentModelBase",
                column: "RandomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaireModels_AdminUserModelLoginId",
                table: "QuestionaireModels",
                column: "AdminUserModelLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionairePages_QuestionaireModelQuestionaireId",
                table: "QuestionairePages",
                column: "QuestionaireModelQuestionaireId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionairePages_RandomGroupId",
                table: "QuestionairePages",
                column: "RandomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroup_QuestionaireModelQuestionaireId",
                table: "RandomGroup",
                column: "QuestionaireModelQuestionaireId");

            migrationBuilder.CreateIndex(
                name: "IX_RandomGroup_QuestionaireModelQuestionaireId1",
                table: "RandomGroup",
                column: "QuestionaireModelQuestionaireId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerModelSubjectUserModel");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "SubjectUsers");

            migrationBuilder.DropTable(
                name: "QuestionaireComponentModelBase");

            migrationBuilder.DropTable(
                name: "QuestionairePages");

            migrationBuilder.DropTable(
                name: "RandomGroup");

            migrationBuilder.DropTable(
                name: "QuestionaireModels");

            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
