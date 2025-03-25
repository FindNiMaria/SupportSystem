using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class OSCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "OSCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CriadoPorId",
                table: "OSCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "OSCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPorId",
                table: "OSCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OSCategory_CriadoPorId",
                table: "OSCategory",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSCategory_ModificadoPorId",
                table: "OSCategory",
                column: "ModificadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OSCategory_AspNetUsers_CriadoPorId",
                table: "OSCategory",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OSCategory_AspNetUsers_ModificadoPorId",
                table: "OSCategory",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSCategory_AspNetUsers_CriadoPorId",
                table: "OSCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_OSCategory_AspNetUsers_ModificadoPorId",
                table: "OSCategory");

            migrationBuilder.DropIndex(
                name: "IX_OSCategory_CriadoPorId",
                table: "OSCategory");

            migrationBuilder.DropIndex(
                name: "IX_OSCategory_ModificadoPorId",
                table: "OSCategory");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "OSCategory");

            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "OSCategory");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "OSCategory");

            migrationBuilder.DropColumn(
                name: "ModificadoPorId",
                table: "OSCategory");
        }
    }
}
