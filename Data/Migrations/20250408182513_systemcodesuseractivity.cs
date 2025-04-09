using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class systemcodesuseractivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_systemCodes_SystemCodeId",
                table: "systemCodeDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "systemCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CriadoPorId",
                table: "systemCodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "systemCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPorId",
                table: "systemCodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "systemCodeDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CriadoPorId",
                table: "systemCodeDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "systemCodeDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPorId",
                table: "systemCodeDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_systemCodes_CriadoPorId",
                table: "systemCodes",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_systemCodes_ModificadoPorId",
                table: "systemCodes",
                column: "ModificadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_systemCodeDetails_CriadoPorId",
                table: "systemCodeDetails",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_systemCodeDetails_ModificadoPorId",
                table: "systemCodeDetails",
                column: "ModificadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CriadoPorId",
                table: "systemCodeDetails",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModificadoPorId",
                table: "systemCodeDetails",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_systemCodes_SystemCodeId",
                table: "systemCodeDetails",
                column: "SystemCodeId",
                principalTable: "systemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_CriadoPorId",
                table: "systemCodes",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModificadoPorId",
                table: "systemCodes",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CriadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModificadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_systemCodes_SystemCodeId",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_CriadoPorId",
                table: "systemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModificadoPorId",
                table: "systemCodes");

            migrationBuilder.DropIndex(
                name: "IX_systemCodes_CriadoPorId",
                table: "systemCodes");

            migrationBuilder.DropIndex(
                name: "IX_systemCodes_ModificadoPorId",
                table: "systemCodes");

            migrationBuilder.DropIndex(
                name: "IX_systemCodeDetails_CriadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropIndex(
                name: "IX_systemCodeDetails_ModificadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "systemCodes");

            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "systemCodes");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "systemCodes");

            migrationBuilder.DropColumn(
                name: "ModificadoPorId",
                table: "systemCodes");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "systemCodeDetails");

            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "systemCodeDetails");

            migrationBuilder.DropColumn(
                name: "ModificadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_systemCodes_SystemCodeId",
                table: "systemCodeDetails",
                column: "SystemCodeId",
                principalTable: "systemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
