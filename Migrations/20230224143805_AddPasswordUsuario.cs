using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WepApiCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorreoElectronico",
                table: "Usuarios",
                newName: "Correo");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Usuarios",
                newName: "CorreoElectronico");
        }
    }
}
