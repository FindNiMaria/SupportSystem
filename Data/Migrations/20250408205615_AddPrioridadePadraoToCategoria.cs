using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPrioridadePadraoToCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrioridadePadraoId",
                table: "TicketCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategories_PrioridadePadraoId",
                table: "TicketCategories",
                column: "PrioridadePadraoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_PrioridadePadraoId",
                table: "TicketCategories",
                column: "PrioridadePadraoId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_PrioridadePadraoId",
                table: "TicketCategories");

            migrationBuilder.DropIndex(
                name: "IX_TicketCategories_PrioridadePadraoId",
                table: "TicketCategories");

            migrationBuilder.DropColumn(
                name: "PrioridadePadraoId",
                table: "TicketCategories");
        }
    }
}
