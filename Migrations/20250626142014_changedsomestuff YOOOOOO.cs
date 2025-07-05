using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Project.Migrations
{
    /// <inheritdoc />
    public partial class changedsomestuffYOOOOOO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NumberFieldStyleId",
                table: "ComponentStyleModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NumberFieldStyleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberType = table.Column<int>(type: "int", nullable: false),
                    MinNumberFloat = table.Column<float>(type: "real", nullable: true),
                    MaxNumberFloat = table.Column<float>(type: "real", nullable: true),
                    MinNumberInteger = table.Column<int>(type: "int", nullable: true),
                    MaxNumberInteger = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberFieldStyleModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel",
                column: "NumberFieldStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentStyleModel_NumberFieldStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel",
                column: "NumberFieldStyleId",
                principalTable: "NumberFieldStyleModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentStyleModel_NumberFieldStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel");

            migrationBuilder.DropTable(
                name: "NumberFieldStyleModel");

            migrationBuilder.DropIndex(
                name: "IX_ComponentStyleModel_NumberFieldStyleId",
                table: "ComponentStyleModel");

            migrationBuilder.DropColumn(
                name: "NumberFieldStyleId",
                table: "ComponentStyleModel");
        }
    }
}
