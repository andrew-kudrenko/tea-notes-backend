using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaNotes.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailfieldbutremovedAvatarUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Users",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "AvatarUrl");
        }
    }
}
