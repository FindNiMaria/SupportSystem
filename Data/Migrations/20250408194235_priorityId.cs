using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class priorityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "PrioridadeId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PrioridadeId",
                table: "Tickets",
                column: "PrioridadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_systemCodeDetails_PrioridadeId",
                table: "Tickets",
                column: "PrioridadeId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_systemCodeDetails_PrioridadeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PrioridadeId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PrioridadeId",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "Prioridade",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
