using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaNotes.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeaNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Taste = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Aroma = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Temperature = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Feeling = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeaNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeaNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeaNoteTastes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kind = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TeaNoteId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_TeaNotes_Title",
                table: "TeaNotes",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeaNotes_UserId",
                table: "TeaNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NickName",
                table: "Users",
                column: "NickName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshSessions");

            migrationBuilder.DropTable(
                name: "TeaNoteTastes");

            migrationBuilder.DropTable(
                name: "TeaNotes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
