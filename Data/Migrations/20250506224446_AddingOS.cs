using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    AssignedToId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssignedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OS_AspNetUsers_AssignedToId1",
                        column: x => x.AssignedToId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OS_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OS_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OS_TicketCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TicketCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OS_TicketSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "TicketSubCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OS_systemCodeDetails_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "systemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OS_systemCodeDetails_StatusId",
                        column: x => x.StatusId,
                        principalTable: "systemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OSComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSComment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OSComment_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OSComment_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OS_AssignedToId1",
                table: "OS",
                column: "AssignedToId1");

            migrationBuilder.CreateIndex(
                name: "IX_OS_CategoryId",
                table: "OS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OS_CreatedById",
                table: "OS",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OS_ModifiedById",
                table: "OS",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OS_PriorityId",
                table: "OS",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_OS_StatusId",
                table: "OS",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OS_SubCategoryId",
                table: "OS",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OSComment_CreatedById",
                table: "OSComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OSComment_ModifiedById",
                table: "OSComment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OSComment_OSId",
                table: "OSComment",
                column: "OSId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OSComment");

            migrationBuilder.DropTable(
                name: "OS");
        }
    }
}
