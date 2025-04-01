using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    public partial class AdicionandoCampoSubCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adicionando a coluna SubCategoryId à tabela Tickets
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Tickets",
                type: "int",
                nullable: true);

            // Criando índice para a nova coluna
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SubCategoryId",
                table: "Tickets",
                column: "SubCategoryId");

            // Adicionando a chave estrangeira
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketSubCategories_SubCategoryId",
                table: "Tickets",
                column: "SubCategoryId",
                principalTable: "TicketSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Removendo a chave estrangeira
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketSubCategories_SubCategoryId",
                table: "Tickets");

            // Removendo o índice
            migrationBuilder.DropIndex(
                name: "IX_Tickets_SubCategoryId",
                table: "Tickets");

            // Removendo a coluna SubCategoryId
            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Tickets");
        }
    }
}
