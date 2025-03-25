using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditTrails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_1_AspNetUsers_CriadoPorId",
                table: "Comment_1");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_1_Ticket_IdChamado",
                table: "Comment_1");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_CriadoPorId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment_1",
                table: "Comment_1");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Comment_1",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_CriadoPorId",
                table: "Tickets",
                newName: "IX_Tickets_CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_1_IdChamado",
                table: "Comment",
                newName: "IX_Comment_IdChamado");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_1_CriadoPorId",
                table: "Comment",
                newName: "IX_Comment_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffectedTable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditTrails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_UserId",
                table: "AuditTrails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tickets_IdChamado",
                table: "Comment",
                column: "IdChamado",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CriadoPorId",
                table: "Tickets",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tickets_IdChamado",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CriadoPorId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comment_1");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CriadoPorId",
                table: "Ticket",
                newName: "IX_Ticket_CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IdChamado",
                table: "Comment_1",
                newName: "IX_Comment_1_IdChamado");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CriadoPorId",
                table: "Comment_1",
                newName: "IX_Comment_1_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_CriadoPorId",
                table: "Ticket",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
