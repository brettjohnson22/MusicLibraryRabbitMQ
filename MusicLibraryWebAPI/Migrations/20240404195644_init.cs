using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicLibraryWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Artist = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "Title" },
                values: new object[,]
                {
                    { 1, "Beck", "The Golden Age" },
                    { 2, "Beck", "Paper Tiger" },
                    { 3, "Beck", "Guess I'm Doing Fine" },
                    { 4, "Beck", "Lonesome Tears" },
                    { 5, "Beck", "Lost Cause" },
                    { 6, "Beck", "End of the Day" },
                    { 7, "Beck", "It's All in Your Mind" },
                    { 8, "Beck", "Round the Bend" },
                    { 9, "Beck", "Already Dead" },
                    { 10, "Beck", "Sunday Sun" },
                    { 11, "Beck", "Little One" },
                    { 12, "Beck", "Side of the Road" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
