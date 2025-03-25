using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Comments2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ticket_IdChamado",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comment_1");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IdChamado",
                table: "Comment_1",
                newName: "IX_Comment_1_IdChamado");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CriadoPorId",
                table: "Comment_1",
                newName: "IX_Comment_1_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment_1",
                table: "Comment_1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_1_AspNetUsers_CriadoPorId",
                table: "Comment_1",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_1_Ticket_IdChamado",
                table: "Comment_1",
                column: "IdChamado",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_1_AspNetUsers_CriadoPorId",
                table: "Comment_1");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_1_Ticket_IdChamado",
                table: "Comment_1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment_1",
                table: "Comment_1");

            migrationBuilder.RenameTable(
                name: "Comment_1",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_1_IdChamado",
                table: "Comment",
                newName: "IX_Comment_IdChamado");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_1_CriadoPorId",
                table: "Comment",
                newName: "IX_Comment_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ticket_IdChamado",
                table: "Comment",
                column: "IdChamado",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
