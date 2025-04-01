using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdSUb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketSubCategories_AspNetUsers_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketSubCategories_AspNetUsers_ModificadoPorId",
                        column: x => x.ModificadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketSubCategories_TicketCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TicketCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketSubCategories_CategoryId",
                table: "TicketSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSubCategories_CriadoPorId",
                table: "TicketSubCategories",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSubCategories_ModificadoPorId",
                table: "TicketSubCategories",
                column: "ModificadoPorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketSubCategories");
        }
    }
}
