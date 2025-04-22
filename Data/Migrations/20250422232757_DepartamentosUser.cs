using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class DepartamentosUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CriadoPorId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Departments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPorId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CriadoPorId",
                table: "Departments",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ModificadoPorId",
                table: "Departments",
                column: "ModificadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_CriadoPorId",
                table: "Departments",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_ModificadoPorId",
                table: "Departments",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_CriadoPorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_ModificadoPorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CriadoPorId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ModificadoPorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ModificadoPorId",
                table: "Departments");
        }
    }
}
