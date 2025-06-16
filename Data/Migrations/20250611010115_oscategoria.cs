using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class oscategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OS_TicketCategories_CategoryId",
                table: "OS");

            migrationBuilder.DropForeignKey(
                name: "FK_OS_TicketSubCategories_SubCategoryId",
                table: "OS");

            migrationBuilder.AddForeignKey(
                name: "FK_OS_OSCategories_CategoryId",
                table: "OS",
                column: "CategoryId",
                principalTable: "OSCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OS_OSSubCategories_SubCategoryId",
                table: "OS",
                column: "SubCategoryId",
                principalTable: "OSSubCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OS_OSCategories_CategoryId",
                table: "OS");

            migrationBuilder.DropForeignKey(
                name: "FK_OS_OSSubCategories_SubCategoryId",
                table: "OS");

            migrationBuilder.AddForeignKey(
                name: "FK_OS_TicketCategories_CategoryId",
                table: "OS",
                column: "CategoryId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OS_TicketSubCategories_SubCategoryId",
                table: "OS",
                column: "SubCategoryId",
                principalTable: "TicketSubCategories",
                principalColumn: "Id");
        }
    }
}
