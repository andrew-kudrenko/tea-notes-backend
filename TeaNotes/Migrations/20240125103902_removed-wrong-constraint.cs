using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaNotes.Migrations
{
    /// <inheritdoc />
    public partial class removedwrongconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeaNotes_Title",
                table: "TeaNotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeaNotes_Title",
                table: "TeaNotes",
                column: "Title",
                unique: true);
        }
    }
}
