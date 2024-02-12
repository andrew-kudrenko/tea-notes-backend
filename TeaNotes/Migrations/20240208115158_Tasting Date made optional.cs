using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaNotes.Migrations
{
    /// <inheritdoc />
    public partial class TastingDatemadeoptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "TastingDate",
                table: "TeaNotes",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "TastingDate",
                table: "TeaNotes",
                type: "Date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
