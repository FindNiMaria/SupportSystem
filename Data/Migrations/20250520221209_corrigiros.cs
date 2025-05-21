using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class corrigiros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OS_AspNetUsers_AssignedToId1",
                table: "OS");

            migrationBuilder.DropIndex(
                name: "IX_OS_AssignedToId1",
                table: "OS");

            migrationBuilder.DropColumn(
                name: "AssignedToId1",
                table: "OS");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedToId",
                table: "OS",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OS_AssignedToId",
                table: "OS",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_OS_AspNetUsers_AssignedToId",
                table: "OS",
                column: "AssignedToId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OS_AspNetUsers_AssignedToId",
                table: "OS");

            migrationBuilder.DropIndex(
                name: "IX_OS_AssignedToId",
                table: "OS");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedToId",
                table: "OS",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId1",
                table: "OS",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OS_AssignedToId1",
                table: "OS",
                column: "AssignedToId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OS_AspNetUsers_AssignedToId1",
                table: "OS",
                column: "AssignedToId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
