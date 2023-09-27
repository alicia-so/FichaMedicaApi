using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class migrations_fichas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Users_UsuarioId",
                table: "Fichas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Fichas",
                newName: "PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Fichas_UsuarioId",
                table: "Fichas",
                newName: "IX_Fichas_PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Users_PacienteId",
                table: "Fichas",
                column: "PacienteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichas_Users_PacienteId",
                table: "Fichas");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Fichas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Fichas_PacienteId",
                table: "Fichas",
                newName: "IX_Fichas_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichas_Users_UsuarioId",
                table: "Fichas",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
