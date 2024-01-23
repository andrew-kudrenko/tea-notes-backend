using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaNotes.Migrations
{
    /// <inheritdoc />
    public partial class Updatedandsyncronizednotestructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeaNoteTastes");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "TeaNotes");

            migrationBuilder.RenameColumn(
                name: "Taste",
                table: "TeaNotes",
                newName: "InfusionTaste");

            migrationBuilder.RenameColumn(
                name: "Feeling",
                table: "TeaNotes",
                newName: "InfusionAroma");

            migrationBuilder.RenameColumn(
                name: "Aroma",
                table: "TeaNotes",
                newName: "InfusionAppearance");

            migrationBuilder.AddColumn<string>(
                name: "AftertasteComment",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "AftertasteDuration",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AftertasteIntensity",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrewingDishware",
                table: "TeaNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrewingMethod",
                table: "TeaNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrewingQuantity",
                table: "TeaNotes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrewingTemperature",
                table: "TeaNotes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrewingVolume",
                table: "TeaNotes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DryLeafAppearance",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DryLeafAroma",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImpressionComment",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ImpressionRate",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpressionWellCombinedWith",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "InfusionBalance",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InfusionBouquet",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InfusionDensity",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InfusionExtractivity",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InfusionTartness",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InfusionViscosity",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "TeaNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturingYear",
                table: "TeaNotes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PricePerGram",
                table: "TeaNotes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TeaTastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kind = table.Column<string>(type: "TEXT", nullable: false),
                    TeaNoteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaTastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeaTastes_TeaNotes_TeaNoteId",
                        column: x => x.TeaNoteId,
                        principalTable: "TeaNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeaTastes_TeaNoteId",
                table: "TeaTastes",
                column: "TeaNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeaTastes");

            migrationBuilder.DropColumn(
                name: "AftertasteComment",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "AftertasteDuration",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "AftertasteIntensity",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "BrewingDishware",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "BrewingMethod",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "BrewingQuantity",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "BrewingTemperature",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "BrewingVolume",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "DryLeafAppearance",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "DryLeafAroma",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "ImpressionComment",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "ImpressionRate",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "ImpressionWellCombinedWith",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionBalance",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionBouquet",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionDensity",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionExtractivity",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionTartness",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "InfusionViscosity",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "ManufacturingYear",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "PricePerGram",
                table: "TeaNotes");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "TeaNotes");

            migrationBuilder.RenameColumn(
                name: "InfusionTaste",
                table: "TeaNotes",
                newName: "Taste");

            migrationBuilder.RenameColumn(
                name: "InfusionAroma",
                table: "TeaNotes",
                newName: "Feeling");

            migrationBuilder.RenameColumn(
                name: "InfusionAppearance",
                table: "TeaNotes",
                newName: "Aroma");

            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "TeaNotes",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TeaNoteTastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeaNoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kind = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaNoteTastes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeaNoteTastes_TeaNotes_TeaNoteId",
                        column: x => x.TeaNoteId,
                        principalTable: "TeaNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeaNoteTastes_TeaNoteId",
                table: "TeaNoteTastes",
                column: "TeaNoteId");
        }
    }
}
