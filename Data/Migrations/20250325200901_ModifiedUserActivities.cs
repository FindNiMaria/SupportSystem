﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedUserActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OSSubCategories");

            migrationBuilder.DropTable(
                name: "TicketSubCategories");

            migrationBuilder.AddColumn<int>(
                name: "IdSubCategoria",
                table: "Tickets",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdSubCategoria",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "OSSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModificadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModificadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
        }
    }
}
