using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpdeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Traducaoetec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tickets_IdChamado",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_CriadoPorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_ModificadoPorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CriadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModificadoPorId",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_CriadoPorId",
                table: "systemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModificadoPorId",
                table: "systemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_AspNetUsers_CriadoPorId",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_AspNetUsers_ModificadoPorId",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_PrioridadePadraoId",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_CriadoPorId",
                table: "TicketResolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_ModificadoPorId",
                table: "TicketResolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CriadoPorId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ModificadoPorId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_CriadoPorId",
                table: "TicketSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_ModificadoPorId",
                table: "TicketSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_TicketCategories_CategoriaId",
                table: "TicketSubCategories");

            migrationBuilder.DropTable(
                name: "OSCategories");

            migrationBuilder.DropTable(
                name: "OSComments");

            migrationBuilder.DropTable(
                name: "OS");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "TicketSubCategories",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "TicketSubCategories",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "TicketSubCategories",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "TicketSubCategories",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "TicketSubCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_ModificadoPorId",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_CriadoPorId",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_CategoriaId",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "Tickets",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "Tickets",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "Tickets",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "Tickets",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ModificadoPorId",
                table: "Tickets",
                newName: "IX_Tickets_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CriadoPorId",
                table: "Tickets",
                newName: "IX_Tickets_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "TicketResolutions",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "TicketResolutions",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "TicketResolutions",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "TicketResolutions",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_TicketResolutions_ModificadoPorId",
                table: "TicketResolutions",
                newName: "IX_TicketResolutions_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_TicketResolutions_CriadoPorId",
                table: "TicketResolutions",
                newName: "IX_TicketResolutions_CreatedById");

            migrationBuilder.RenameColumn(
                name: "PrioridadePadraoId",
                table: "TicketCategories",
                newName: "DefaultPriorityId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TicketCategories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "TicketCategories",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "TicketCategories",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "TicketCategories",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "TicketCategories",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "TicketCategories",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_PrioridadePadraoId",
                table: "TicketCategories",
                newName: "IX_TicketCategories_DefaultPriorityId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_ModificadoPorId",
                table: "TicketCategories",
                newName: "IX_TicketCategories_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_CriadoPorId",
                table: "TicketCategories",
                newName: "IX_TicketCategories_CreatedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "systemCodes",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "systemCodes",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "systemCodes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "systemCodes",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "systemCodes",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "systemCodes",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodes_ModificadoPorId",
                table: "systemCodes",
                newName: "IX_systemCodes_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodes_CriadoPorId",
                table: "systemCodes",
                newName: "IX_systemCodes_CreatedById");

            migrationBuilder.RenameColumn(
                name: "PedidoNo",
                table: "systemCodeDetails",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "systemCodeDetails",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "systemCodeDetails",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "systemCodeDetails",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "systemCodeDetails",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "systemCodeDetails",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "systemCodeDetails",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodeDetails_ModificadoPorId",
                table: "systemCodeDetails",
                newName: "IX_systemCodeDetails_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodeDetails_CriadoPorId",
                table: "systemCodeDetails",
                newName: "IX_systemCodeDetails_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Departments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ModificadoPorId",
                table: "Departments",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModificadoEm",
                table: "Departments",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "Departments",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "Departments",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "Codigo",
                table: "Departments",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_ModificadoPorId",
                table: "Departments",
                newName: "IX_Departments_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_CriadoPorId",
                table: "Departments",
                newName: "IX_Departments_CreatedById");

            migrationBuilder.RenameColumn(
                name: "IdChamado",
                table: "Comment",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Comment",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CriadoPorId",
                table: "Comment",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "Comment",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IdChamado",
                table: "Comment",
                newName: "IX_Comment_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CriadoPorId",
                table: "Comment",
                newName: "IX_Comment_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PrimeiroNome",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "AspNetUsers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "AspNetUsers",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Comment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ModifiedById",
                table: "Comment",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CreatedById",
                table: "Comment",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_ModifiedById",
                table: "Comment",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tickets_TicketId",
                table: "Comment",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedById",
                table: "Departments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_ModifiedById",
                table: "Departments",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CreatedById",
                table: "systemCodeDetails",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModifiedById",
                table: "systemCodeDetails",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_CreatedById",
                table: "systemCodes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModifiedById",
                table: "systemCodes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_AspNetUsers_CreatedById",
                table: "TicketCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_AspNetUsers_ModifiedById",
                table: "TicketCategories",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_DefaultPriorityId",
                table: "TicketCategories",
                column: "DefaultPriorityId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_CreatedById",
                table: "TicketResolutions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_ModifiedById",
                table: "TicketResolutions",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CreatedById",
                table: "Tickets",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById",
                table: "Tickets",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_CreatedById",
                table: "TicketSubCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_ModifiedById",
                table: "TicketSubCategories",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_TicketCategories_CategoryId",
                table: "TicketSubCategories",
                column: "CategoryId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CreatedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_ModifiedById",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tickets_TicketId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedById",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_ModifiedById",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CreatedById",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModifiedById",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_CreatedById",
                table: "systemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModifiedById",
                table: "systemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_AspNetUsers_CreatedById",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_AspNetUsers_ModifiedById",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_DefaultPriorityId",
                table: "TicketCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_CreatedById",
                table: "TicketResolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_ModifiedById",
                table: "TicketResolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_CreatedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_CreatedById",
                table: "TicketSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_ModifiedById",
                table: "TicketSubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketSubCategories_TicketCategories_CategoryId",
                table: "TicketSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ModifiedById",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "TicketSubCategories",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "TicketSubCategories",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "TicketSubCategories",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TicketSubCategories",
                newName: "CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TicketSubCategories",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_ModifiedById",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_CreatedById",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketSubCategories_CategoryId",
                table: "TicketSubCategories",
                newName: "IX_TicketSubCategories_CategoriaId");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Tickets",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Tickets",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Tickets",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tickets",
                newName: "CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ModifiedById",
                table: "Tickets",
                newName: "IX_Tickets_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_CreatedById",
                table: "Tickets",
                newName: "IX_Tickets_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "TicketResolutions",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "TicketResolutions",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "TicketResolutions",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TicketResolutions",
                newName: "CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketResolutions_ModifiedById",
                table: "TicketResolutions",
                newName: "IX_TicketResolutions_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketResolutions_CreatedById",
                table: "TicketResolutions",
                newName: "IX_TicketResolutions_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TicketCategories",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "TicketCategories",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "TicketCategories",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "DefaultPriorityId",
                table: "TicketCategories",
                newName: "PrioridadePadraoId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "TicketCategories",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TicketCategories",
                newName: "CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "TicketCategories",
                newName: "Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_ModifiedById",
                table: "TicketCategories",
                newName: "IX_TicketCategories_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_DefaultPriorityId",
                table: "TicketCategories",
                newName: "IX_TicketCategories_PrioridadePadraoId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketCategories_CreatedById",
                table: "TicketCategories",
                newName: "IX_TicketCategories_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "systemCodes",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "systemCodes",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "systemCodes",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "systemCodes",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "systemCodes",
                newName: "CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "systemCodes",
                newName: "Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodes_ModifiedById",
                table: "systemCodes",
                newName: "IX_systemCodes_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodes_CreatedById",
                table: "systemCodes",
                newName: "IX_systemCodes_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "systemCodeDetails",
                newName: "PedidoNo");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "systemCodeDetails",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "systemCodeDetails",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "systemCodeDetails",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "systemCodeDetails",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "systemCodeDetails",
                newName: "CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "systemCodeDetails",
                newName: "Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodeDetails_ModifiedById",
                table: "systemCodeDetails",
                newName: "IX_systemCodeDetails_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_systemCodeDetails_CreatedById",
                table: "systemCodeDetails",
                newName: "IX_systemCodeDetails_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Departments",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Departments",
                newName: "ModificadoEm");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Departments",
                newName: "ModificadoPorId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Departments",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Departments",
                newName: "CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Departments",
                newName: "Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_ModifiedById",
                table: "Departments",
                newName: "IX_Departments_ModificadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_CreatedById",
                table: "Departments",
                newName: "IX_Departments_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Comment",
                newName: "IdChamado");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Comment",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Comment",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Comment",
                newName: "CriadoPorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TicketId",
                table: "Comment",
                newName: "IX_Comment_IdChamado");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CreatedById",
                table: "Comment",
                newName: "IX_Comment_CriadoPorId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Sobrenome");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "PrimeiroNome");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Pais");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "AspNetUsers",
                newName: "Genero");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "AspNetUsers",
                newName: "Cidade");

            migrationBuilder.CreateTable(
                name: "OS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OS_AspNetUsers_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OSCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModificadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSCategories_AspNetUsers_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OSCategories_AspNetUsers_ModificadoPorId",
                        column: x => x.ModificadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OSComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoPorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdOS = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OSComments_AspNetUsers_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OSComments_OS_IdOS",
                        column: x => x.IdOS,
                        principalTable: "OS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OS_CriadoPorId",
                table: "OS",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSCategories_CriadoPorId",
                table: "OSCategories",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSCategories_ModificadoPorId",
                table: "OSCategories",
                column: "ModificadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSComments_CriadoPorId",
                table: "OSComments",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_OSComments_IdOS",
                table: "OSComments",
                column: "IdOS");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CriadoPorId",
                table: "Comment",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tickets_IdChamado",
                table: "Comment",
                column: "IdChamado",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_CriadoPorId",
                table: "Departments",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_ModificadoPorId",
                table: "Departments",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CriadoPorId",
                table: "systemCodeDetails",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModificadoPorId",
                table: "systemCodeDetails",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_CriadoPorId",
                table: "systemCodes",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodes_AspNetUsers_ModificadoPorId",
                table: "systemCodes",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_AspNetUsers_CriadoPorId",
                table: "TicketCategories",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_AspNetUsers_ModificadoPorId",
                table: "TicketCategories",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketCategories_systemCodeDetails_PrioridadePadraoId",
                table: "TicketCategories",
                column: "PrioridadePadraoId",
                principalTable: "systemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_CriadoPorId",
                table: "TicketResolutions",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketResolutions_AspNetUsers_ModificadoPorId",
                table: "TicketResolutions",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_CriadoPorId",
                table: "Tickets",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ModificadoPorId",
                table: "Tickets",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_CriadoPorId",
                table: "TicketSubCategories",
                column: "CriadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_AspNetUsers_ModificadoPorId",
                table: "TicketSubCategories",
                column: "ModificadoPorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketSubCategories_TicketCategories_CategoriaId",
                table: "TicketSubCategories",
                column: "CategoriaId",
                principalTable: "TicketCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
