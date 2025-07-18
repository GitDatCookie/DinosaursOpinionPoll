using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class Stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIConversationModel_SubjectUsers_UserId",
                table: "AIConversationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AIMessageModel_AIConversationModel_ConversationId",
                table: "AIMessageModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentStyleModel_NumberFieldStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_ComponentStyleModel_ComponentStyleId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberFieldStyleModel",
                table: "NumberFieldStyleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentStyleModel",
                table: "ComponentStyleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AIMessageModel",
                table: "AIMessageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AIConversationModel",
                table: "AIConversationModel");

            migrationBuilder.RenameTable(
                name: "NumberFieldStyleModel",
                newName: "NumberStyleModels");

            migrationBuilder.RenameTable(
                name: "ComponentStyleModel",
                newName: "StyleModels");

            migrationBuilder.RenameTable(
                name: "AIMessageModel",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "AIConversationModel",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentStyleModel_NumberFieldStyleId",
                table: "StyleModels",
                newName: "IX_StyleModels_NumberFieldStyleId");

            migrationBuilder.RenameIndex(
                name: "IX_AIMessageModel_ConversationId",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_AIConversationModel_UserId",
                table: "Conversations",
                newName: "IX_Conversations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberStyleModels",
                table: "NumberStyleModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StyleModels",
                table: "StyleModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_SubjectUsers_UserId",
                table: "Conversations",
                column: "UserId",
                principalTable: "SubjectUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_StyleModels_ComponentStyleId",
                table: "QuestionaireComponentModelBase",
                column: "ComponentStyleId",
                principalTable: "StyleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StyleModels_NumberStyleModels_NumberFieldStyleId",
                table: "StyleModels",
                column: "NumberFieldStyleId",
                principalTable: "NumberStyleModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_SubjectUsers_UserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionaireComponentModelBase_StyleModels_ComponentStyleId",
                table: "QuestionaireComponentModelBase");

            migrationBuilder.DropForeignKey(
                name: "FK_StyleModels_NumberStyleModels_NumberFieldStyleId",
                table: "StyleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StyleModels",
                table: "StyleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NumberStyleModels",
                table: "NumberStyleModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "StyleModels",
                newName: "ComponentStyleModel");

            migrationBuilder.RenameTable(
                name: "NumberStyleModels",
                newName: "NumberFieldStyleModel");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "AIMessageModel");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "AIConversationModel");

            migrationBuilder.RenameIndex(
                name: "IX_StyleModels_NumberFieldStyleId",
                table: "ComponentStyleModel",
                newName: "IX_ComponentStyleModel_NumberFieldStyleId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "AIMessageModel",
                newName: "IX_AIMessageModel_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_UserId",
                table: "AIConversationModel",
                newName: "IX_AIConversationModel_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentStyleModel",
                table: "ComponentStyleModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NumberFieldStyleModel",
                table: "NumberFieldStyleModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AIMessageModel",
                table: "AIMessageModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AIConversationModel",
                table: "AIConversationModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AIConversationModel_SubjectUsers_UserId",
                table: "AIConversationModel",
                column: "UserId",
                principalTable: "SubjectUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AIMessageModel_AIConversationModel_ConversationId",
                table: "AIMessageModel",
                column: "ConversationId",
                principalTable: "AIConversationModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentStyleModel_NumberFieldStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel",
                column: "NumberFieldStyleId",
                principalTable: "NumberFieldStyleModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionaireComponentModelBase_ComponentStyleModel_ComponentStyleId",
                table: "QuestionaireComponentModelBase",
                column: "ComponentStyleId",
                principalTable: "ComponentStyleModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
