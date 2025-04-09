using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class categoriatickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CategoriaId",
                table: "Tickets",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_CategoriaId",
                table: "Tickets",
                column: "CategoriaId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_CategoriaId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CategoriaId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Tickets");
        }
    }
}
