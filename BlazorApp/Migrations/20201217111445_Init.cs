using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Languages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "_Movies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Filename = table.Column<string>(type: "TEXT", nullable: true),
                    LanguageID = table.Column<Guid>(type: "TEXT", nullable: true),
                    IMDBIndex = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Movies__Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "_Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Genres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MovieID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genres", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Genres__Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "_Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_MovieMetadatas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MovieID = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserID = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastPosition = table.Column<double>(type: "REAL", nullable: false),
                    ViewCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MovieMetadatas", x => x.ID);
                    table.ForeignKey(
                        name: "FK__MovieMetadatas__Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "_Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MovieMetadatas__Users_UserID",
                        column: x => x.UserID,
                        principalTable: "_Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "_Subtitles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageID = table.Column<Guid>(type: "TEXT", nullable: true),
                    MovieID = table.Column<Guid>(type: "TEXT", nullable: true),
                    Filename = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subtitles", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Subtitles__Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "_Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Subtitles__Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "_Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Genres_MovieID",
                table: "_Genres",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX__MovieMetadatas_MovieID",
                table: "_MovieMetadatas",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX__MovieMetadatas_UserID",
                table: "_MovieMetadatas",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX__Movies_LanguageID",
                table: "_Movies",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX__Subtitles_LanguageID",
                table: "_Subtitles",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX__Subtitles_MovieID",
                table: "_Subtitles",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Genres");

            migrationBuilder.DropTable(
                name: "_MovieMetadatas");

            migrationBuilder.DropTable(
                name: "_Subtitles");

            migrationBuilder.DropTable(
                name: "_Users");

            migrationBuilder.DropTable(
                name: "_Movies");

            migrationBuilder.DropTable(
                name: "_Languages");
        }
    }
}
