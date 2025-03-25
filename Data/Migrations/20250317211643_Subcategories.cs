using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Subcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSCategory_AspNetUsers_CriadoPorId",
                table: "OSCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_OSCategory_AspNetUsers_ModificadoPorId",
                table: "OSCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OSCategory",
                table: "OSCategory");

            migrationBuilder.RenameTable(
                name: "OSCategory",
                newName: "OSCategories");

            migrationBuilder.RenameIndex(
                name: "IX_OSCategory_ModificadoPorId",
                table: "OSCategories",
                newName: "IX_OSCategories_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_OSCategory_CriadoPorId",
                table: "OSCategories",
                newName: "IX_OSCategories_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OSCategories",
                table: "OSCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OSSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModificadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSSubCategories_AspNetUsers_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OSSubCategories_AspNetUsers_ModificadoPorId",
                        column: x => x.ModificadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OSSubCategories_OSCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "OSCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_OSSubCategories_CategoryId",
                table: "OSSubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OSSubCategories_CriadoPorId",
                table: "OSSubCategories",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSSubCategories_ModificadoPorId",
                table: "OSSubCategories",
                column: "ModificadoPorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OSCategories_AspNetUsers_CriadoPorId",
                table: "OSCategories",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OSCategories_AspNetUsers_ModificadoPorId",
                table: "OSCategories",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OSCategories_AspNetUsers_CriadoPorId",
                table: "OSCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_OSCategories_AspNetUsers_ModificadoPorId",
                table: "OSCategories");

            migrationBuilder.DropTable(
                name: "OSSubCategories");

            migrationBuilder.DropTable(
                name: "TicketSubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OSCategories",
                table: "OSCategories");

            migrationBuilder.RenameTable(
                name: "OSCategories",
                newName: "OSCategory");

            migrationBuilder.RenameIndex(
                name: "IX_OSCategories_ModificadoPorId",
                table: "OSCategory",
                newName: "IX_OSCategory_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_OSCategories_CriadoPorId",
                table: "OSCategory",
                newName: "IX_OSCategory_CriadoPorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OSCategory",
                table: "OSCategory",
                column: "Id");

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
    }
}
