using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class assigning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_CategoriaId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_systemCodeDetails_PrioridadeId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Tickets",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PrioridadeId",
                table: "Tickets",
                newName: "PriorityId");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Tickets",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Tickets",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Anexo",
                table: "Tickets",
                newName: "Attachment");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PrioridadeId",
                table: "Tickets",
                newName: "IX_Tickets_PriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CategoriaId",
                table: "Tickets",
                newName: "IX_Tickets_CategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedOn",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedToId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId1",
                table: "Tickets",
                column: "AssignedToId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedToId1",
                table: "Tickets",
                column: "AssignedToId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryId",
                table: "Tickets",
                column: "CategoryId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_systemCodeDetails_PriorityId",
                table: "Tickets",
                column: "PriorityId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssignedToId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_systemCodeDetails_PriorityId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssignedToId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssignedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssignedToId1",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tickets",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "Tickets",
                newName: "PrioridadeId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tickets",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tickets",
                newName: "CategoriaId");

            migrationBuilder.RenameColumn(
                name: "Attachment",
                table: "Tickets",
                newName: "Anexo");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PriorityId",
                table: "Tickets",
                newName: "IX_Tickets_PrioridadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CategoryId",
                table: "Tickets",
                newName: "IX_Tickets_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_CategoriaId",
                table: "Tickets",
                column: "CategoriaId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_systemCodeDetails_PrioridadeId",
                table: "Tickets",
                column: "PrioridadeId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
