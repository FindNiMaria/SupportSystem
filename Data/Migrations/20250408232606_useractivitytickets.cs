using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class useractivitytickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEm",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPorId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ModificadoPorId",
                table: "Tickets",
                column: "ModificadoPorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ModificadoPorId",
                table: "Tickets",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ModificadoPorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ModificadoPorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModificadoEm",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModificadoPorId",
                table: "Tickets");
        }
    }
}
